namespace DDDNETBB.Domain.Abstractions
{
    public interface IEventedAggregateRoot : IAggregateRoot, IEventedEntity
    { }
}