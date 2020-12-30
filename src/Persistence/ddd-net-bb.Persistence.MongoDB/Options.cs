using System.ComponentModel;

namespace DDDNETBB.Persistence.MongoDB
{
    public sealed class Options
    {
        public const string ConfigSectionName = "mongodb";

        public string ConnectionString { get; init; } = "localhost";
        public string DatabaseName { get; set; }
        public string CollectionName { get; init; }
        public bool Seed { get; init; } = false;

        [Description("Might be helpful for the integration testing.")]
        public bool SetRandomDatabaseSuffix { get; set; }
    }
}