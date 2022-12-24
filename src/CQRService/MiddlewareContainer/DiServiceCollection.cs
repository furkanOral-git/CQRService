using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CQRService.ExceptionHandling.MiddlewareContainerExceptions;
using CQRService.MiddlewareContainer.Entities;
using CQRService.MiddlewareContainer.Factories.Abstract;
using CQRService.MiddlewareContainer.Factories.Concrete;

namespace CQRService.MiddlewareContainer
{
    public class DiServiceCollection : IDiServiceCollection
    {
        private IServiceRegisterFactory _factory;
        private static DiServiceCollection? _instance;


        private DiServiceCollection()
        {
            _factory = ServiceRegisterFactory.GetFactory();
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
            Guid id = Guid.NewGuid();
            
            var serviceRegister = _factory.GetServiceRegister
            (
                id,
                null,
                typeof(TImplementation),
                RegistrationType.Singleton,
                out bool IsNew
            );
            if (!IsNew)
            {
                _factory.UpdateServiceRegister(serviceRegister);
            }


        }

        public void AddTransient<TImplementation>()
        where TImplementation : class
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));
            Guid id = Guid.NewGuid();
            
            var serviceRegister = _factory.GetServiceRegister
            (
                id,
                null,
                typeof(TImplementation),
                RegistrationType.Transient,
                out bool IsNew
            );
            if (!IsNew)
            {
                _factory.UpdateServiceRegister(serviceRegister);
            }

        }

        public void AddSingleton<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));
            Guid id = Guid.NewGuid();
            
            var serviceRegister = _factory.GetServiceRegister
            (
                id,
                typeof(TSource),
                typeof(TImplementation),
                RegistrationType.Singleton,
                out bool IsNew
            );
            if (!IsNew)
            {
                _factory.UpdateServiceRegister(serviceRegister);
            }

        }

        public void AddTransient<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource
        {
            VerifyTypeForIsNotAbstract(typeof(TImplementation));
            Guid id = Guid.NewGuid();
            
            var serviceRegister = _factory.GetServiceRegister
            (
                id,
                typeof(TSource),
                typeof(TImplementation),
                RegistrationType.Transient,
                out bool IsNew
            );
            if (!IsNew)
            {
                _factory.UpdateServiceRegister(serviceRegister);
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