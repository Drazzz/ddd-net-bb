using System;
using Microsoft.Extensions.DependencyInjection;

namespace DDDNETBB.Persistence.EF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEFStore(this IServiceCollection services)
        {
            if(services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IDomainDBContextFactory, DomainDBContextFactory>();

            return services;
        }

    }
}