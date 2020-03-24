using System;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Utils.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T Resolve<T>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = serviceProvider.GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return service;
        }
    }
}
