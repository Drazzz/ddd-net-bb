using System.Collections.Generic;
using DDDNETBB.Core.Abstractions;

namespace DDDNETBB.Domain.Abstractions
{
    public interface IEventedEntity
    {
        IReadOnlyCollection<IEvent> GetUncommittedChanges();
        void MarkChangesAsCommitted();
    }
}