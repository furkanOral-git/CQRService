using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRService.Middleware.Responses;

namespace CQRService
{
    public interface IInterceptorResultStack 
    {
        public void AddInterceptorResult(string sender, object data);
        
    }
}