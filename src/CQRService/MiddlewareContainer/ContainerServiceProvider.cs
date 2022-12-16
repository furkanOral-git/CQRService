using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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
            var args = GetArgs(serviceRegister.ImplementationType);
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
        private object[]? GetArgs(Type impType)
        {
            var parameterInfos = impType
            .GetConstructors()[0]
            .GetParameters();

            var hasArgs = (parameterInfos.Length != 0) ? true : false;
            if (hasArgs)
            {
                return parameterInfos.Select(p => p.ParameterType).Select(t => GetService(t)).ToArray();
            }
            return default;
        }

        object IRuntimeServiceProvider.GetServiceOnRuntime(Type sourceType)
        {
            var serviceRegister = _container.RegisteredTypes.SingleOrDefault(r => r.SourceType == sourceType);
            if (serviceRegister is null)
            {
                throw new NotRegisteredTypeException(MiddlewareContainerExceptionMessages.NotRegisteredTypeExceptionMessage);
            }
            var serviceInstance = _factory.GetServiceInstance(serviceRegister.InstanceId);
            if (serviceInstance.Instance is null)
            {
                return GetService(serviceRegister.ImplementationType);
            }
            return serviceInstance.Instance;
        }
        public TService GetService<TService>() where TService : class
        {
            return (TService)GetService(typeof(TService));
        }
    }
}