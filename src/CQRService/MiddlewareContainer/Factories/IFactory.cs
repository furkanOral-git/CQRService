using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.MiddlewareContainer.Entities;

namespace CQRService.MiddlewareContainer.Factories
{
    internal interface IFactory<T> where T : ContainerEntity
    {
        
    }
}