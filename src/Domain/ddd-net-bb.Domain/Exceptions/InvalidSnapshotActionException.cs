using DDDNETBB.Domain.Abstractions;

namespace DDDNETBB.Domain.Exceptions
{
    public sealed class InvalidSnapshotActionException : DomainException
    {
        public InvalidSnapshotActionException() : base("Cannot apply snapshot on an already loaded aggregate") {}
        public InvalidSnapshotActionException(string message) : base(message) {}
    }
}