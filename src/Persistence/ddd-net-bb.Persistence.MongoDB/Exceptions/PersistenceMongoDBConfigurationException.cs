using System;

namespace DDDNETBB.Persistence.MongoDB.Exceptions
{
    public sealed class PersistenceMongoDBConfigurationException : ApplicationException
    {
        internal PersistenceMongoDBConfigurationException(string message) : base(message) { }
        internal PersistenceMongoDBConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}