# **Contents**

## 1. Service Registration

## 2. Requests

## 3. Interceptors

## 4. ExceptionHandling

## 5. Responses


<br/>

## **1. Service Registration**
---
This library use its own dependency injection module for minimal dependency to other projects. All handlers will get their dependencies from  **ContainerDistributionController** class. This class inherited from **IDistributionController**.
Service registration operations are manage by **DiServiceCollection** class that implements **IDiServiceCollection**.

<br>

>**! Warning :** Service dependencies other than the library will not be provided by the distributor. If needed service can be called by controller.

<br/>

- ### Module Based Registration
Modules that inject to services and dependencies should be inherited from InjectionModuleBase

### Usage of **InjectionModuleBase** abstract class :

<br/>

```cs
using CQRService.MiddlewareContainer.InjectionModules;

namespace Project.Application.InjectionModules
{
    public class ApplicationModule : InjectionModuleBase
    {
        public override void LoadServices(IDiServiceCollection services)
        {
            //requiered for handlers
            services.AddSingleton<IProductRepository,EfProductRepository>();
            services.AddSingleton<IUserRepository,EfUserRepository>();

            //this registrations are necessary
            services.AddSingleton<CreateProductRequestHandler>();
            //or you can add like;
            services.AddTransient<GetProductByIdQueryHandler>();
        }
    }
}

```
Then,

```cs
using CQRService.MiddlewareContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DiServiceCollection.GetServiceCollections()
.AddModule<ApplicationModule>()
.AddModule<AnotherModule>();

builder.Services.AddControllers();

...

```
<br/>

## **2. Requests**
---
Requests must be implement one of them that **IRequest** or **IQuery** interfaces. Their handlers must be declared in it as "nested". **IRequestHandler** or **IQueryHandler** are handler interfaces of requests.<br/><br/>

### Example Implementation of a Request :

<br/>

```cs

public partial class CreateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
        {
            private IProductRepository _repo;

            public CreateProductCommandHandler(IProductRepository repo)
            {
                _repo = repo;
            }
            public void Handle(CreateProductCommand request)
            {
                var product = new Product();
                product.Id = request.Id;
                product.Name = request.Name;
                product.Price = request.Price;
                _repo.Add(product);
            }
        }
    }

```
And

### Example Implementation of a Query :
<br/>

```cs

public partial class GetProductByIdQuery : IQuery<Product>
{
    public int Id { get; set; }

    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product>
    {
        private IProductRepository _repo;

        public GetProductByIdQueryHandler(IProductRepository repo)
        {
                _repo = repo;
        }
        public Product Handle(GetProductByIdQuery query)
        {
                return _repo.GetBy(p => p.Id == query.Id);
        }
    }
}

```
Queries have differences according to requests on handling. **IQueryHandler<TQuery,TResponse>**

>TResponse : Return type of query.

<br/>

## **3. Interceptors**
---

Interceptors are as you know by their name work when handler's handle method run. This library's middleware receive the request and  return a object that implements **IMiddlewareResponse** interface. We can input objects what you want to this response object and also can use for run specific operation on request's handler operations.

<br/>

For declare a interceptor you can use **RequestInterceptor** abstract class by inherit from it. This class has four virtual method.

<br/>

>--
>
> **OnBefore :** Include code block going to run on handle methods before
>
> **OnException :** If any exception occured in handle method, Included code blocks going to run.
>
> **OnSuccess :** That specified code blocks will be finished as succeed handle method operation otherwise on exception state this'll not run.
>
> **OnAfter :** 
> 
>--
