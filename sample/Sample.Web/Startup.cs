using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Sample.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<Context.SampleDbContext>(builder =>
            {
                builder.UseInMemoryDatabase();
            }, ServiceLifetime.Transient);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddUnitOfWork(provider =>
            {
                provider.Register(new SampleUnitOfWorkRegisteration());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using (var context = app.ApplicationServices.GetService<Context.SampleDbContext>())
            {
                if (context.Database.EnsureCreated())
                {
                    var role = new Entity.Role
                    {
                        Name = "Admin"
                    };
                    context.Add(role);
                    context.SaveChanges();

                    var user = new Entity.User
                    {
                        Username = "test",
                        Nickname = "Test User",
                        Password = "111111",
                        RoleID = role.ID
                    };
                    context.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
