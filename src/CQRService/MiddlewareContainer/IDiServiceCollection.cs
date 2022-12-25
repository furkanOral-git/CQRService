using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CQRService.MiddlewareContainer
{
    public interface IDiServiceCollection
    {
        public void AddSingleton<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource;

        public void AddSingleton<TImplementation>()
        where TImplementation : class, new();

        public void AddTransient<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource;

        public void AddTransient<TImplementation>()
        where TImplementation : class, new();

        public void AddScoped<TSource, TImplementation>()
        where TSource : class
        where TImplementation : class, TSource;

        public void AddScoped<TImplementation>()
        where TImplementation : class, new();
    }
}