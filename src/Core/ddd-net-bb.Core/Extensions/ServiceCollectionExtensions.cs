using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDNETBB.Core
{
    public static class ServiceCollectionExtensions
    {
        public static TModel GetOptions<TModel>(this IServiceCollection serviceCollection, string sectionName)
            where TModel : new()
        {
            if (serviceCollection is null)
                throw new ArgumentNullException(nameof(serviceCollection));
            if (sectionName.IsNullEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(sectionName));

            var configuration = GetConfigurationInstance(serviceCollection);
            var model = new TModel();

            configuration.GetSection(sectionName)
                .Bind(model);

            return model;
        }


        private static IConfiguration GetConfigurationInstance(IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IConfiguration>();
        }
    }
}