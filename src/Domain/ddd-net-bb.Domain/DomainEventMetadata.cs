using System;
using DDDNETBB.Core;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain
{
    public sealed class DomainEventMetadata
    {
        public DateTime CreationDate {get;}
        public Guid EventId {get;}


        private DomainEventMetadata(Guid eventId, DateTime creationDate)
        {
            if(eventId.IsEmpty())
                throw new InvalidDomainMetadataArgumentException(nameof(eventId));

            EventId = eventId;
            CreationDate = creationDate;
        }
        public static DomainEventMetadata For(Guid eventId, DateTime creationDate) => new DomainEventMetadata(eventId, creationDate);
        public static DomainEventMetadata Default() => new DomainEventMetadata(Guid.NewGuid(), DateTime.UtcNow);
    }
}