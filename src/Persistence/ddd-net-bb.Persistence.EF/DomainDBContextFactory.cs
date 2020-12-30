using System;
using DDDNETBB.Core.Abstractions.Dependencies;
using DDDNETBB.Persistence.EF.Exceptions;
using Microsoft.Extensions.Logging;

namespace DDDNETBB.Persistence.EF
{
    public interface IDomainDBContextFactory
    {
        EntityFrameworkDbContext CreateDBContext();
        EntityFrameworkDbContext CreateDBContext<TProvider>() where TProvider : IDatabaseProvider;
    }


    public sealed class DomainDBContextFactory : IDomainDBContextFactory
    {
        private readonly IResolver _resolver;
        private readonly ILogger<DomainDBContextFactory> _logger;


        public DomainDBContextFactory(IResolver resolver, ILogger<DomainDBContextFactory> loggerFactory)
        {
            _logger = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }


        public EntityFrameworkDbContext CreateDBContext() => CreateDBContext<IDatabaseProvider>();

        public EntityFrameworkDbContext CreateDBContext<TProvider>() where TProvider : IDatabaseProvider
        {
            var dbProvider = _resolver.Resolve<TProvider>();
            if(dbProvider is null)
                throw new PersistenceEntityFrameworkConfigurationException("Database Provider not found.");

            _logger.LogInformation($"DBContex resolved using: {dbProvider}", nameof(TProvider));

            return dbProvider.CreateDBContext();
        }
    }
}