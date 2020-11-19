namespace DDDNETBB.Domain.Tests.Fakes
{
    internal class FakeDomainEvent : DomainEvent
    {
        public string Name {get;}


        public FakeDomainEvent(DomainEventMetadata metadata, string name) : base(metadata)
            => Name = name;   
        public FakeDomainEvent() : this(DomainEventMetadata.Default()) { }
        public FakeDomainEvent(DomainEventMetadata metadata) : this(metadata, null) { }
    }
}