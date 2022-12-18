using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRService.Entities.Interceptors
{
    public record InterceptorResult(string AspectType, string Message, object AspectData);

}