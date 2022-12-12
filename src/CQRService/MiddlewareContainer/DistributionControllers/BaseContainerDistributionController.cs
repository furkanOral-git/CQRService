using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling.MiddlewareContainerExceptions;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.InstanceControllers;
using CQRService.MiddlewareContainer.RegistrationControllers;

namespace CQRService.MiddlewareContainer.DistributionControllers
{
    public abstract class BaseContainerDistributionController
    {
        private static ContainerRegistrationController _registrationController;
        private static ContainerInstanceController _instanceController;

        static BaseContainerDistributionController()
        {
            _registrationController = (ContainerRegistrationController)GetServiceStatic(typeof(ContainerRegistrationController));
            _instanceController = (ContainerInstanceController)GetServiceStatic(typeof(ContainerInstanceController));
        }
        internal ContainerRegistrationController GetRegistrationController()
        {
            return _registrationController;
        }
        internal ContainerInstanceController GetInstanceController()
        {
            return _instanceController;
        }
        private static object GetServiceBase(ServiceRegister register, object[]? args = null)
        {
            ServiceInstance instance;
            if (_instanceController.TryGetServiceInstanceClass(register.InstanceId, out instance))
            {
                switch (register.RegistrationType)
                {
                    case RegistrationType.Singleton:
                        break;
                    case RegistrationType.Transient:
                        var newObject = Activator.CreateInstance(register.ImplementationType, args ?? null);
                        instance.UpdateInstance(newObject);
                        break;
                    case RegistrationType.Scoped:
                        //if request id will different then create instance
                        //but if it's same request then return same instance
                        //create instance per request
                        //i need an requestId
                        break;
                }
                return instance.Instance;
            }
            var value = Activator.CreateInstance(register.ImplementationType, args ?? null);
            _instanceController.CreateServiceInstanceClass(register.InstanceId, value);
            return value;
        }
        internal static object GetServiceStatic(Type sourceType)
        {
            ServiceRegister register;
            if (_registrationController.TryGetServiceRegister(sourceType, out register))
            {

                var parameterTypes = register.ImplementationType
                    .GetConstructors()[0]
                    .GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray();

                if (parameterTypes.Length > 0)
                {
                    var arguments = parameterTypes.Select(t => GetServiceStatic(t)).ToArray();
                    return GetServiceBase(register, arguments);
                }
                return GetServiceBase(register);
            }
            throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage + $"SubjectType : {sourceType.Name}");
        }
        internal static object GetServiceOnRuntimeStatic(Type sourceType)
        {
            ServiceRegister register;
            if (_registrationController.TryGetServiceRegister(sourceType, out register))
            {
                return GetServiceOnRuntimeBase(register);
            }
            throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage + $"SubjectType : {sourceType.Name}");
        }
        private static object GetServiceOnRuntimeBase(ServiceRegister register)
        {
            ServiceInstance instance;
            if (_instanceController.TryGetServiceInstanceClass(register.InstanceId, out instance))
            {
                return instance.Instance;
            }
            var parameterTypes = register.ImplementationType
               .GetConstructors()[0]
               .GetParameters()
               .Select(p => p.ParameterType)
               .ToArray();

            object[] args = null;

            if (parameterTypes.Length > 0)
            {
                args = parameterTypes.Select(t => GetServiceOnRuntimeStatic(t)).ToArray();
            }
            var value = Activator.CreateInstance(register.ImplementationType, args ?? null);
            _instanceController.CreateServiceInstanceClass(register.InstanceId, value);
            return value;
        }

    }
}