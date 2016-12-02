# Shared.Infrastructure

A infrastructure framework for ASP.NET Core.
There is a sample project, refer to [DotNetClub](https://github.com/scheshan/DotNetClub)

### Get Start

You can download the package by NuGet, use following script
```
Install-Package Shared.Infrastructure
```

There are two packages to support UnitOfWorkPattern
```
Install-Package Shared.Infrastructure.EntityFramework
Install-Package Shared.Infrastructure.Cassandra
```

### Add DependencyInjection Factory

Call UseInfrastructureFactory() extension method on WebHostBuilder, and a global ServiceProviderFactory based on AutoFac will be registered.
```
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseInfrastructureFactory()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
```

In your Startup class, you can populate two methods, ConfigureServices and ConfigureContainer. You can register system dependencies with IServiceCollection, and register your own dependencies with ContainerBuilder
```
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }
```

### Add UnitOfWork

In your Startup class, you can register UnitOfWork dependencies in ConfigureContainer method.

```
        builder.AddUnitOfWork()
            .AddEntityFramework<ClubContext>(UnitOfWorkNames.EntityFramework);
```
> If you want to use EntityFramework UnitOfWork pattern, make sure you register the context in the ConfigureServices method.
> ```
        services.AddDbContext<ClubContext>(builder =>
            {
                builder.UseSqlServer(this.Configuration["ConnectionString"], options =>
                {
                });
            }, ServiceLifetime.Transient);
```
