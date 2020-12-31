using System;
using DDDNETBB.Core;
using DDDNETBB.Domain;
using DDDNETBB.Persistence.MongoDB.Builders;
using DDDNETBB.Persistence.MongoDB.Factories;
using DDDNETBB.Persistence.MongoDB.Initializers;
using DDDNETBB.Persistence.MongoDB.Repositories;
using DDDNETBB.Persistence.MongoDB.Seeders;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DDDNETBB.Persistence.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Helpful when dealing with integration testing
        private static bool _conventionsRegistered;

        public static IServiceCollection AddMongoDb(this IServiceCollection services,
            string optionsSectionName = Options.ConfigSectionName,
            Type seederType = null,
            bool registerConventions = true)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (optionsSectionName.IsNullEmptyOrWhitespace())
                optionsSectionName = Options.ConfigSectionName;

            var options = services.GetOptions<Options>(optionsSectionName);
            return services.AddMongoDb(options, seederType, registerConventions);
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services,
            Func<IMongoDbOptionsBuilder, IMongoDbOptionsBuilder> buildOptions,
            string optionsSectionName = null,
            Type seederType = null,
            bool registerConventions = true)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            var optionsBuilder = optionsSectionName.IsNullEmptyOrWhitespace()
                ? MongoDbOptionsBuilder.New()
                : MongoDbOptionsBuilder.From(services.GetOptions<Options>(optionsSectionName));
            var mongoOptions = buildOptions(optionsBuilder).Build();

            return services.AddMongoDb(mongoOptions, seederType, registerConventions);
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services,
            Options mongoOptions,
            Type seederType = null,
            bool registerConventions = true)
        {
            if (mongoOptions.SetRandomDatabaseSuffix)
                ApplyDatabaseSuffix(mongoOptions);

            services.AddSingleton(mongoOptions);
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<Options>();
                return new MongoClient(options?.ConnectionString);
            });
            services.AddTransient(sp =>
            {
                var options = sp.GetService<Options>();
                var client = sp.GetService<IMongoClient>();

                return client?.GetDatabase(options.DatabaseName);
            });

            services
                .AddTransient<IMongoDbInitializer, MongoDbInitializer>()
                .AddTransient<IMongoDbSessionFactory, MongoDbSessionFactory>()
                .AddTransient<IMongoDbInitializer, MongoDbInitializer>()
                ;

            ApplySeeder(services, seederType);

            if (registerConventions && !_conventionsRegistered)
                RegisterConventions();
            
            return services;
        }

        public static IServiceCollection AddMongoDbReadRepository<TAggregateRoot, TKey>(this IServiceCollection services,
            string collectionName)
            where TAggregateRoot : EventedAggregateRoot<TKey>
            where TKey: Identity
        {
            services.AddTransient<IMongoDbQueryRepository<TAggregateRoot, TKey>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoDbQueryRepository<TAggregateRoot, TKey>(database, collectionName);
            });

            return services;
        }

        public static IServiceCollection AddMongoDbWriteRepository<TAggregateRoot, TKey>(
            this IServiceCollection services,
            string collectionName)
            where TAggregateRoot : EventedAggregateRoot<TKey>
            where TKey : Identity
        {
            services.AddTransient<IMongoDbWriteRepository<TAggregateRoot, TKey>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoDbWriteRepository<TAggregateRoot, TKey>(database, collectionName);
            });

            return services;
        }


        private static void ApplySeeder(IServiceCollection services, Type seederType)
        {
            if (seederType is null)
                services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
            else
                services.AddTransient(typeof(IMongoDbSeeder), seederType);
        }

        private static void RegisterConventions()
        {
            _conventionsRegistered = true;

            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("ddd-net-bb", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            }, _ => true);
        }


        private static void ApplyDatabaseSuffix(Options mongoOptions)
        {
            var suffix = $"{Guid.NewGuid():N}";
            Console.WriteLine($"Setting random suffix {suffix} for {mongoOptions.DatabaseName}");

            mongoOptions.DatabaseName = $"{mongoOptions.DatabaseName}_{suffix}";
        }
    }
}