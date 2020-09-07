using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domodedovo.PhoneBook.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection,
            Action<DbContextOptionsBuilder> optionsAction,
            IList<Assembly> autoMapperAssemblies)
        {
            autoMapperAssemblies.Add(typeof(ServiceCollectionExtensions).Assembly);

            return serviceCollection.AddDbContext<UsersDbContext>(optionsAction);
        }
    }
}