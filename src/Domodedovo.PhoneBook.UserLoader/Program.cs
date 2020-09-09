using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Core.Extensions;
using Domodedovo.PhoneBook.Data.Extensions;
using Domodedovo.PhoneBook.Integrations.RandomUser.Extensions;
using Domodedovo.PhoneBook.UserLoader.Actions;
using Domodedovo.PhoneBook.UserLoader.Options;
using Domodedovo.PhoneBook.UserLoader.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.UserLoader
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            Configure(configurationBuilder, args);
            var configuration = configurationBuilder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var appHost = serviceProvider.GetRequiredService<AppHost>();

            var exitCode = await appHost.Run();
            return (int) exitCode;
        }

        private static void Configure(IConfigurationBuilder configurationBuilder, string[] args)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args, ConsoleCommandOptions.SwitchMappings)
                .AddJsonFile("appsettings.json");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var mediatRAssemblies = new List<Assembly>();
            var autoMapperAssemblies = new List<Assembly>();

            var randomUserApiUrl = new Uri(configuration["Integrations:RandomUser:ApiUrl"]);

            serviceCollection.Configure<ConsoleCommandOptions>(configuration);

            serviceCollection.AddLogging(builder =>
                builder.AddConsole().AddConfiguration(configuration.GetSection("Logging")));

            serviceCollection
                .AddSingleton<AppHost>()
                .AddTransient<IAppActionService, AppActionService>()
                .AddTransient<IAppActionFactory, AppActionFactory>()
                .AddDataServices(options => options.UseSqlServer(configuration.GetConnectionString("Default")),
                    autoMapperAssemblies)
                .AddCoreServices(mediatRAssemblies)
                .AddRandomUserServices(randomUserApiUrl, mediatRAssemblies, autoMapperAssemblies);

            serviceCollection.AddMediatR(mediatRAssemblies.ToArray());
            serviceCollection.AddAutoMapper(autoMapperAssemblies.ToArray());
        }
    }
}