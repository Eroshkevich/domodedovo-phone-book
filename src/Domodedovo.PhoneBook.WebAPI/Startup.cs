using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Domodedovo.PhoneBook.Core.CQRS;
using Domodedovo.PhoneBook.Core.Extensions;
using Domodedovo.PhoneBook.Data;
using Domodedovo.PhoneBook.Data.Extensions;
using Domodedovo.PhoneBook.WebAPI.ModelBinders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Domodedovo.PhoneBook.WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.ModelBinderProviders.Insert(0, new SortingParameterCollectionBinderProvider()));

            var xmlCommentsFilePath =
                Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(xmlCommentsFilePath);

                c.MapType<ICollection<SortingParameter<GetUsersQuerySortingKey>>>(() =>
                    new OpenApiSchema
                    {
                        Type = "string",
                        Format = "string"
                    });
            });

            var autoMapperAssemblies = new List<Assembly>();
            var mediatRAssemblies = new List<Assembly>();

            services.AddDataServices(
                    options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName)),
                    autoMapperAssemblies)
                .AddCoreServices(mediatRAssemblies);

            services.AddAutoMapper(autoMapperAssemblies.ToArray());
            services.AddMediatR(mediatRAssemblies.ToArray());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}