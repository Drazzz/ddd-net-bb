using DDDNETBB.Domain.Abstractions;

namespace DDDNETBB.Domain.Exceptions
{
    public sealed class InvalidDomainEventArgumentException : DomainException
    {
        public InvalidDomainEventArgumentException(string paramName)
            : base($"Cannot add null DomainEvent: {paramName}")
        { }
    } 
}