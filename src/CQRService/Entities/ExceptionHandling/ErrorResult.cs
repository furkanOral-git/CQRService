using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CQRService.Entities.ExceptionHandling
{
    public record struct ErrorResult(
    string Title,
    string Sender,
    string ErrorMessage,
    string ExceptionType,
    HttpStatusCode Status);

}