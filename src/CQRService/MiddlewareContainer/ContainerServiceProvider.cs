using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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
        private static ContainerServiceProvider? _instance;
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
        internal object GetService(Type sourceType, string callingTarget = "")
        {
            var serviceRegister = _container.RegisteredTypes.SingleOrDefault(r => r.SourceType == sourceType || r.ImplementationType == sourceType);
            if (serviceRegister is null)
            {
                throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage);
            }
            var args = GetArgs(serviceRegister.ImplementationType, serviceRegister.ImplementationType.Name, GetService);
            var serviceInstance = (serviceRegister.RegistrationType == RegistrationType.Scoped)
            ? _factory.GetScopedServiceInstance(serviceRegister.InstanceId, GetTarget(callingTarget))
            : _factory.GetServiceInstance(serviceRegister.InstanceId);

            return GetInstanceByRegisterType(serviceRegister, serviceInstance, args);

        }
        private string GetTarget(string callingTarget = "")
        {
            if (callingTarget is not "") return callingTarget;
            StackTrace trace = new StackTrace();
            MethodBase? method = trace.GetFrame(2)?.GetMethod();

            var isGetService = method?.Name == "GetService";
            if (isGetService) method = trace.GetFrame(3)?.GetMethod();

            StringBuilder target = new();

            if (method.IsConstructor)
            {
                target = target.Append(method?.DeclaringType?.Name);
                return target.ToString();
            }
            target = target.Append(method?.DeclaringType?.Name + "_" + method?.Name);
            return target.ToString();

        }
        private object GetInstanceByRegisterType(ServiceRegister serviceRegister, ServiceInstance serviceInstance, object[]? args)
        {
            object? instance = default;
            if (serviceInstance.Instance is null)
            {
                instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                serviceInstance.Instance = instance;
                return instance;
            }
            switch (serviceRegister.RegistrationType)
            {
                case RegistrationType.Singleton:
                    instance = serviceInstance.Instance;
                    break;
                case RegistrationType.Transient:
                    instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                    serviceInstance.Instance = instance;
                    break;
                case RegistrationType.Scoped:
                    instance = serviceInstance.Instance;
                    break;
            }
            if (instance is null)
            {
                throw new Exception("An error occured when creating new instance");
            }
            return instance;
        }

        private object[]? GetArgs(Type impType, string callingTarget, Func<Type, string, object?> selectOperation)
        {
            var parameterInfos = impType
            .GetConstructors()[0]
            .GetParameters();

            var hasArgs = (parameterInfos.Length != 0) ? true : false;
            if (hasArgs)
            {
                return parameterInfos.Select(p => p.ParameterType).Select(t => selectOperation.Invoke(t, callingTarget)).ToArray();
            }
            return default;
        }
        private object? GetServiceOnRuntimeBase(Type sourceType, string callingTarget)
        {
            var serviceRegister = _container.RegisteredTypes.SingleOrDefault(r => r.SourceType == sourceType || r.ImplementationType == sourceType);
            if (serviceRegister is null)
            {
                throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage);
            }
            var serviceInstance = (serviceRegister.RegistrationType == RegistrationType.Scoped)
            ? _factory.GetScopedServiceInstance(serviceRegister.InstanceId, callingTarget)
            : _factory.GetServiceInstance(serviceRegister.InstanceId);

            if (serviceInstance.Instance is null)
            {
                var args = GetArgs(serviceRegister.ImplementationType, callingTarget, GetServiceOnRuntimeBase);
                var instance = Activator.CreateInstance(serviceRegister.ImplementationType, args ?? null);
                serviceInstance.Instance = instance;
            }
            return serviceInstance.Instance;
        }
        object? IRuntimeServiceProvider.GetServiceOnRuntime(Type sourceType, string callingTarget = "")
        {
            return GetServiceOnRuntimeBase(sourceType, GetTarget(callingTarget));
        }
        public TService GetService<TService>() where TService : class
        {
            return (TService)GetService(typeof(TService));
        }


    }
}