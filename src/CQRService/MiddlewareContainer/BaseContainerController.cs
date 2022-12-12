using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer
{
    internal abstract class BaseContainerController
    {
        protected static MiddlewareServiceContainer _container;

        static BaseContainerController()
        {
            _container = new MiddlewareServiceContainer();
        }
       
    }
}