using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRService.Runtime;

namespace CQRService.MiddlewareContainer.DistributionControllers
{
    public class ContainerDistributionController : BaseContainerDistributionController, IDistributionController, IRuntimeDistributionController
    {

        private ContainerDistributionController()
        {
            
        }
        public object GetService(Type sourceType)
        {
            return BaseContainerDistributionController.GetServiceStatic(sourceType);
        }
        public TService GetService<TService>()
        where TService : class
        {
            return (TService)BaseContainerDistributionController.GetServiceStatic(typeof(TService));
        }

        object IRuntimeDistributionController.GetServiceOnRuntime(Type sourceType)
        {
            return BaseContainerDistributionController.GetServiceOnRuntimeStatic(sourceType);
        }
    }

}
