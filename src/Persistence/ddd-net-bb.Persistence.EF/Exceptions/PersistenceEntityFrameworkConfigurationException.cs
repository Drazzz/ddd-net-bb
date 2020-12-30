using System;

namespace DDDNETBB.Persistence.EF.Exceptions
{
    public sealed class PersistenceEntityFrameworkConfigurationException : ApplicationException
    {
        internal PersistenceEntityFrameworkConfigurationException(string message) : base(message) { }
        internal PersistenceEntityFrameworkConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}