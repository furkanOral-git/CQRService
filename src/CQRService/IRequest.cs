using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Entities.BaseInterfaces;

namespace CQRService
{
    public interface IRequest<TEntity> : IRequestBase, IRequestQueryBase<TEntity>
    where TEntity : class, new()
    {
        
    }
}