using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling.MiddlewareContainerExceptions;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.Factories.Abstract;
using CQRService.MiddlewareContainer.Factories.Concrete;
using CQRService.Runtime;

namespace CQRService.MiddlewareContainer
{
    public sealed class ContainerServiceProvider : IRuntimeServiceProvider, IDiServiceProvider
    {
        private static ContainerServiceProvider _instance;
        private MiddlewareServiceContainer _container;
        private IServiceInstanceFactory _factory;

        private ContainerServiceProvider()
        {
            _container = MiddlewareServiceContainer.InitContainer();
            _factory = ServiceInstanceFactory.GetFactory();
        }
        public static ContainerServiceProvider GetProvider()
        {
            if (_instance is null) _instance = new ContainerServiceProvider();
            return _instance;
        }
        public object GetService(Type sourceType)
        {
            var serviceRegister = _container.RegisteredTypes.SingleOrDefault(r => r.SourceType == sourceType || r.ImplementationType == sourceType);
            if (serviceRegister is null)
            {
                throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage);
            }
            var args = GetArgs(serviceRegister.ImplementationType,GetService);
            var serviceInstance = _factory.GetServiceInstance(serviceRegister.InstanceId);
            return GetInstanceByRegisterType(serviceRegister, serviceInstance, args);
            
        }
        private object GetInstanceByRegisterType(ServiceRegister serviceRegister, ServiceInstance serviceInstance, object[]? args)
        {
            object instance = default;
            if (serviceInstance.Instance is null)
            {
                instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                serviceInstance.UpdateInstance(instance);
                return instance;
            }
            switch (serviceRegister.RegistrationType)
            {
                case RegistrationType.Singleton:
                    instance = serviceInstance.Instance;
                    break;
                case RegistrationType.Transient:
                    instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                    serviceInstance.UpdateInstance(instance);
                    break;

            }
            if (instance is null)
            {
                throw new Exception("An error occured when creating new instance");
            }
            return instance;
        }
        private object[]? GetArgs(Type impType, Func<Type, object> selectOperation)
        {
            var parameterInfos = impType
            .GetConstructors()[0]
            .GetParameters();

            var hasArgs = (parameterInfos.Length != 0) ? true : false;
            if (hasArgs)
            {
                return parameterInfos.Select(p => p.ParameterType).Select(t => selectOperation.Invoke(t)).ToArray();
            }
            return default;
        }
        private object GetServiceOnRuntimeBase(Type sourceType)
        {
            var serviceRegister = _container.RegisteredTypes.SingleOrDefault(r => r.SourceType == sourceType || r.ImplementationType == sourceType);
            if (serviceRegister is null)
            {
                throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage);
            }
            var serviceInstance = _factory.GetServiceInstance(serviceRegister.InstanceId);
            if (serviceInstance.Instance is null)
            {
                var args = GetArgs(serviceRegister.ImplementationType, GetServiceOnRuntimeBase);
                var instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                serviceInstance.UpdateInstance(instance);
            }
            return serviceInstance.Instance;
        }
        object IRuntimeServiceProvider.GetServiceOnRuntime(Type sourceType)
        {
            return GetServiceOnRuntimeBase(sourceType);
        }
        public TService GetService<TService>() where TService : class
        {
            return (TService)GetService(typeof(TService));
        }
    }
}