using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CpuApi.ModelBinders
{
    /// <summary>
    /// The model binder for delimited arrays.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />
    public class ArrayModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
        /// <returns>
        /// <para>
        /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
        /// </para>
        /// <para>
        /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
        /// </para>
        /// <para>
        /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
        /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
        /// </para>
        /// </returns>
        /// <exception cref="ArgumentNullException">bindingContext</exception>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var values = valueProviderResult
                .ToString()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var elementType = bindingContext.ModelType.IsConstructedGenericType
                ? bindingContext.ModelType.GetGenericArguments()[0]
                : bindingContext.ModelType.GetElementType();

            if (values.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(Array.CreateInstance(elementType, 0));
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(elementType);
                var typedArray = Array.CreateInstance(elementType, values.Length);

                try
                {
                    for (int i = 0; i < values.Length; ++i)
                    {
                        var value = converter.ConvertFromString(values[i]);
                        typedArray.SetValue(value, i);
                    }
                }
                catch (Exception exception)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, exception, bindingContext.ModelMetadata);
                }

                bindingContext.Result = ModelBindingResult.Success(typedArray);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// The provider for array model binder.
        /// </summary>
        /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinderProvider" />
        public class Provider : IModelBinderProvider
        {
            /// <summary>
            /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" /> based on <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.
            /// </summary>
            /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.</param>
            /// <returns>
            /// An <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />.
            /// </returns>
            /// <exception cref="ArgumentNullException">context</exception>
            public IModelBinder GetBinder(ModelBinderProviderContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (context.Metadata.IsEnumerableType && !context.Metadata.ElementMetadata.IsComplexType)
                {
                    return new ArrayModelBinder();
                }

                return null;
            }
        }
    }
}
