using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CQRService.Entities.ExceptionHandling;
using CQRService.Entities.Interceptors;
using CQRService.Middleware.Responses;

namespace CQRService.JsonProviderService
{
    public static class JsonProviderService
    {
        public static JsonElement GetResponseAsJSON(this IMiddlewareResponse response)
        {
            return GetAsJsonBy<MiddlewareBaseResponse>(response);
        }
        public static JsonElement GetResultsAsJSON(this IResultStackAccessor accessor)
        {
            return GetAsJsonBy<InterceptorResultStack>(accessor);
        }
        public static JsonElement GetErrorsAsJSON(this IErrorStackAccessor accessor)
        {
            return GetAsJsonBy<ErrorStack>(accessor);
        }
        public static JsonElement GetAsJsonBy<T>(object? value)
        {
            var jsonElement = JsonSerializer.SerializeToElement(value, typeof(T),
            new JsonSerializerOptions() { WriteIndented = true }
            );
            return jsonElement;
        }


    }
}