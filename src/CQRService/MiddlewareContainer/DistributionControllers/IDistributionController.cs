using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace CQRService.MiddlewareContainer.DistributionControllers
{
    public interface IDistributionController
    {
        public object GetService(Type sourceType);
        public TService GetService<TService>()
        where TService : class;
        
    }
}