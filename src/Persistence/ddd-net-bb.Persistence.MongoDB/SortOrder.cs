using System.Collections.Generic;
using DDDNETBB.Core;
using Newtonsoft.Json;

namespace DDDNETBB.Persistence.MongoDB
{
    public class SortOrder : Enumeration
    {
        [JsonConstructor]
        private SortOrder(int id, string name)
            : base(id, name)
        { }


        public static SortOrder Ascending = new(1, nameof(Ascending).ToLowerInvariant());
        public static SortOrder Descending = new(2, nameof(Descending).ToLowerInvariant());


        public static SortOrder From(string name) =>
            name?.Length > 3 ? FromDisplayName<SortOrder>(name?.ToLowerInvariant()) : FromShortName(name);
        public static SortOrder From(int id) => FromValue<SortOrder>(id);
        public static IReadOnlyCollection<SortOrder> List() => new[] { Ascending, Descending };


        private static SortOrder FromShortName(string name) => name?.ToLowerInvariant() == "asc" ? Ascending : Descending;
    }
}