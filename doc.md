# **Contents**

## 1. Service Registration

## 2. Requests

## 3. Interceptors

## 4. ExceptionHandling

## 5. Responses

---
<br/>

## **1. Service Registration**
This library use its own dependency injection module for minimal dependency to other projects. All handlers'll get their dependencies from  **ContainerDistributionController** class. This class inherited from **IDistributionController**.
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
## **2. Requests**
Requests must be imlement one of them that **IRequest** or **IQuery** interfaces. Their handlers must be declared in it as "nested". **IRequestHandler** or **IQueryHandler** are handler interfaces of requests.<br/><br/>

### Usage of IRequest and IQuery interfaces:

<br/>

```cs

```

