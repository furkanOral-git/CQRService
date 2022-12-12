using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Entities.BaseInterfaces
{
    public interface IRequestQueryBase<TEntity> : IRequestQueryBase
    where TEntity : class, new()
    {

    }
    public interface IRequestQueryBase
    {
        
    }
}