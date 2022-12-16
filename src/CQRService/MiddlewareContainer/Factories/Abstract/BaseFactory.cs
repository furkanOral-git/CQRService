using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.Factories.Abstract
{
    internal abstract class BaseFactory
    {
        protected static MiddlewareServiceContainer Services { get; private set; }

        static BaseFactory()
        {
            Services = MiddlewareServiceContainer.InitContainer();
        }
    }
}