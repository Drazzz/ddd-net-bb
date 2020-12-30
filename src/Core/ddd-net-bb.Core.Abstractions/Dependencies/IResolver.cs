using System;
using System.Collections.Generic;

namespace DDDNETBB.Core.Abstractions.Dependencies
{
    public interface IResolver
    {
        TType Resolve<TType>();
        IEnumerable<TType> ResolveAll<TType>();

        object Resolve(Type type);
        IEnumerable<object> ResolveAll(Type type);
    }
}