using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.MiddlewareContainer
{
    public interface IDiServiceProvider
    {
        public TService GetService<TService>() where TService : class;
        internal void NewRequestId();


    }
}