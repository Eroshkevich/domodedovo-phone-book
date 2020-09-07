using System;
using System.Collections.Generic;
using System.Reflection;
using Domodedovo.PhoneBook.Integrations.RandomUser.Options;
using Domodedovo.PhoneBook.Integrations.RandomUser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRandomUserServices(this IServiceCollection serviceCollection, Uri apiUrl,
            IList<Assembly> mediatRAssemblies, IList<Assembly> autoMapperAssemblies)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            mediatRAssemblies.Add(assembly);
            autoMapperAssemblies.Add(assembly);

            serviceCollection.AddHttpClient<IRandomUserClient, RandomUserClient>(client =>
                client.BaseAddress = apiUrl);

            return serviceCollection.AddSingleton<IJsonSerializerOptionsProvider, JsonSerializerOptionsProvider>();
        }
    }
}