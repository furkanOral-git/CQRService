using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling.MiddlewareContainerExceptions;
using CQRService.MiddlewareContainer.DistributionControllers;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.RegistrationControllers;

namespace CQRService.MiddlewareContainer
{
    public class DiServiceCollection : IDiServiceCollection
    {
        private ContainerRegistrationController _registrationController;
        private static DiServiceCollection _instance;
        

        private DiServiceCollection()
        {
            _registrationController = (ContainerRegistrationController)BaseContainerDistributionController
            .GetServiceStatic(typeof(ContainerRegistrationController));
        }
        public static DiServiceCollection InitServiceCollection()
        {
            if (_instance is null)
            {
                _instance = new DiServiceCollection();
            }
            return _instance;
        }
        public void AddSingleton<TImplementation>()
        where TImplementation : class
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));

            var guid = Guid.NewGuid();
            var serviceRegister = _registrationController.CreateServiceRegisterIfNotExistAndReturn
            (
                guid,
                null,
                typeof(TImplementation),
                RegistrationType.Singleton
            );
            if (serviceRegister.InstanceId != guid)
            {
                _registrationController.UpdateServiceRegister(serviceRegister.InstanceId, typeof(TImplementation), RegistrationType.Singleton);
            }

        }

        public void AddTransient<TImplementation>()
        where TImplementation : class
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));

            var guid = Guid.NewGuid();
            var serviceRegister = _registrationController.CreateServiceRegisterIfNotExistAndReturn
            (
                guid,
                null,
                typeof(TImplementation),
                RegistrationType.Transient
            );
            if (serviceRegister.InstanceId != guid)
            {
                _registrationController.UpdateServiceRegister(serviceRegister.InstanceId, typeof(TImplementation), RegistrationType.Transient);
            }
        }

        public void AddSingleton<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));

            var guid = Guid.NewGuid();
            var serviceRegister = _registrationController.CreateServiceRegisterIfNotExistAndReturn
            (
                guid,
                typeof(TSource),
                typeof(TImplementation),
                RegistrationType.Singleton
            );
            if (serviceRegister.InstanceId != guid)
            {
                _registrationController.UpdateServiceRegister(serviceRegister.InstanceId, typeof(TImplementation), RegistrationType.Singleton);
            }
        }

        public void AddTransient<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));

            var guid = Guid.NewGuid();
            var serviceRegister = _registrationController.CreateServiceRegisterIfNotExistAndReturn
            (
                guid,
                typeof(TSource),
                typeof(TImplementation),
                RegistrationType.Transient
            );
            if (serviceRegister.InstanceId != guid)
            {
                _registrationController.UpdateServiceRegister(serviceRegister.InstanceId, typeof(TImplementation), RegistrationType.Transient);
            }
        }
        private void VerifyTypeForIsNotAbstract(Type type)
        {
            if (type.IsAbstract)
            {
                throw new AbstractImplementationTypeException
                (MiddlewareContainerExceptionMessages.AbstractImplementationTypeMessage + $"AbstractImplementationType : {type.Name}");
            }
        }
    }
}