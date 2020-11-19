using DDDNETBB.Domain.Abstractions;

namespace DDDNETBB.Domain.Exceptions
{
    public sealed class InvalidDomainMetadataArgumentException : DomainException
    {
        public InvalidDomainMetadataArgumentException(string paramName)
            : base($"Cannot create DomainMetadata! Argument: {paramName} is not valid.")
        { }
    }
}