using System;
using System.Collections.Generic;
using DDDNETBB.Core.Abstractions.Dependencies;
using Microsoft.Extensions.DependencyInjection;


namespace DDDNETBB.Core.Dependencies
{
    public sealed class Resolver : IResolver
    {
        private readonly IServiceProvider _serviceProvider;


        public Resolver(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));


        public TType Resolve<TType>() => _serviceProvider.GetService<TType>();

        public IEnumerable<TType> ResolveAll<TType>() => _serviceProvider.GetServices<TType>();

        public object Resolve(Type type) => _serviceProvider.GetService(type);

        public IEnumerable<object> ResolveAll(Type type) => _serviceProvider.GetServices(type);
    }
}