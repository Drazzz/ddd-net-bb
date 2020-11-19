using System;
using DDDNETBB.Core.Abstractions;
using DDDNETBB.Domain.Abstractions;
using MediatR;

namespace DDDNETBB.Domain
{
    public abstract class DomainEvent : IDomainEvent
        , IMetadataProvider<DomainEventMetadata>
        , INotification
    {
        public DomainEventMetadata Metadata {get;}
        public DateTime OccurredOn => Metadata.CreationDate;
        public Guid Id => Metadata.EventId;


        protected DomainEvent(DomainEventMetadata metadata) => Metadata = metadata ?? DomainEventMetadata.Default();
    }
}