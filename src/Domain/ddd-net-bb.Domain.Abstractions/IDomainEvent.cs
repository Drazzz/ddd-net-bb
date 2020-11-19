using System;
using DDDNETBB.Core.Abstractions;

namespace DDDNETBB.Domain.Abstractions
{
    public interface IDomainEvent : IEvent
    {
        DateTime OccurredOn {get;}
    }
}