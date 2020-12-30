namespace DDDNETBB.Persistence.EF
{
    public sealed class Options
    {
        public const string ConfigSectionName = "sql";

        
        public string ConnectionString { get; init; } = "localhost";
        /// <summary>
        ///     Command Timeout in minutes
        /// </summary>
        public int? CommandTimeout { get; init; } = 1;
        public RetryOptions Retry { get; init; }


        public sealed class RetryOptions
        {
            public bool? EnableRetryOnFailure { get; init; }
            public int? MaxRetryCount { get; init; }
            /// <summary>
            ///     Retry timeout in seconds
            /// </summary>
            public int? MaxRetryDelay { get; init; }
        }
    }  
}