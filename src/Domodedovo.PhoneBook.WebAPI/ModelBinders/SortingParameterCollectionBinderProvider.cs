using System.Collections.Generic;
using Domodedovo.PhoneBook.Core.CQRS;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domodedovo.PhoneBook.WebAPI.ModelBinders
{
    public class SortingParameterCollectionBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder binder = new SortingParameterCollectionBinder();
            return context.Metadata.ModelType == typeof(ICollection<SortingParameter<GetUsersQuerySortingKey>>)
                ? binder
                : null;
        }
    }
}