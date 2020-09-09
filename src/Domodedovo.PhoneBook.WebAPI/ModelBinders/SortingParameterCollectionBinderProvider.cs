using System;
using System.Collections.Generic;
using System.Linq;
using Domodedovo.PhoneBook.Core.CQRS;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domodedovo.PhoneBook.WebAPI.ModelBinders
{
    public class SortingParameterCollectionBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.ModelType.IsGenericType 
                || !context.Metadata.ModelType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ICollection<>))) 
                return null;
            
            var collectionItemType = context.Metadata.ModelType.GetGenericArguments().Single();

            if (!collectionItemType.IsGenericType ||
                collectionItemType.GetGenericTypeDefinition() != typeof(SortingParameter<>)) 
                return null;
                
            var types = collectionItemType.GetGenericArguments();
            var o = typeof(SortingParameterCollectionBinder<>).MakeGenericType(types);

            return (IModelBinder) Activator.CreateInstance(o);

        }
    }
}