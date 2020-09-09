using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domodedovo.PhoneBook.WebAPI.ModelBinders
{
    public class SortingParameterCollectionBinder : IModelBinder
    {
        private const string DescendingFlag = "desc";

        private static Task SuccessResult(ModelBindingContext bindingContext,
            ICollection<SortingParameter<GetUsersQuerySortingKey>> sortingParameters = null)
        {
            bindingContext.Result = ModelBindingResult.Success(sortingParameters);
            return Task.CompletedTask;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var sortingProviderResult = bindingContext.ValueProvider.GetValue("sorting");

            if (sortingProviderResult == ValueProviderResult.None)
                return SuccessResult(bindingContext);

            var sortingValue = sortingProviderResult.FirstValue;
            if (string.IsNullOrWhiteSpace(sortingValue))
                return SuccessResult(bindingContext);

            var sortingParameters = sortingValue.Split(';').Select(s =>
                {
                    var isDescending = false;
                    var stringBuilder = new StringBuilder(s);

                    if (s.EndsWith(DescendingFlag))
                    {
                        isDescending = true;
                        stringBuilder.Remove(s.Length - DescendingFlag.Length, DescendingFlag.Length);
                    }

                    var sortingKeyString = stringBuilder.ToString().Trim();

                    if (Enum.TryParse<GetUsersQuerySortingKey>(sortingKeyString, true, out var sortingKey))
                    {
                        return new SortingParameter<GetUsersQuerySortingKey>
                        {
                            SortingKey = sortingKey,
                            IsDescending = isDescending
                        };
                    }

                    throw new Exception();
                }
            );

            return SuccessResult(bindingContext, sortingParameters.ToList());
        }
    }
}