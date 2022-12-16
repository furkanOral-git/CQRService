using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer.Entities
{
    public abstract class ContainerEntity
    {
        public Guid InstanceId { get; init; }
    }
}