using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CQRService.Entities.ExceptionHandling
{
    public record ErrorResult(
    string Title,
    string CatcherType,
    string ErrorMessage,
    string ExceptionType,
    HttpStatusCode Status);

}