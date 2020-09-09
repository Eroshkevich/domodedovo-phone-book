using System.Collections.Generic;
using System.Reflection;
using Domodedovo.PhoneBook.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domodedovo.PhoneBook.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection,
            IList<Assembly> mediatRAssemblies)
        {
            mediatRAssemblies.Add(typeof(ServiceCollectionExtensions).Assembly);

            serviceCollection.AddHttpClient<IBinaryLoadingService, BinaryLoadingService>();

            return serviceCollection.AddTransient<IFileStorageService, FileStorageService>();
        }
    }
}