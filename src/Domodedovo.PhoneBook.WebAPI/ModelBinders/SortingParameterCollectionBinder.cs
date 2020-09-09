using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domodedovo.PhoneBook.WebAPI.ModelBinders
{
    public class SortingParameterCollectionBinder<T> : IModelBinder where T : struct, Enum
    {
        private const string DescendingFlag = "desc";

        private static Task Result(ModelBindingContext bindingContext,
            ICollection<SortingParameter<T>> sortingParameters = null)
        {
            bindingContext.Result = ModelBindingResult.Success(sortingParameters);
            return Task.CompletedTask;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var sortingProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

            if (sortingProviderResult == ValueProviderResult.None)
                return Result(bindingContext);

            var sortingValue = sortingProviderResult.FirstValue;
            if (string.IsNullOrWhiteSpace(sortingValue))
                return Result(bindingContext);

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

                    if (Enum.TryParse<T>(sortingKeyString, true, out var sortingKey))
                    {
                        return new SortingParameter<T>
                        {
                            SortingKey = sortingKey,
                            IsDescending = isDescending
                        };
                    }

                    bindingContext.ModelState.AddModelError(bindingContext.FieldName,
                        $"Invalid sorting key '{sortingKeyString}'");
                    return null;
                }
            );

            return Result(bindingContext, sortingParameters.ToList());
        }
    }
}