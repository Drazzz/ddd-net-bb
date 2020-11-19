using System;

namespace DDDNETBB.Core
{
    public static class GuidExtension
    {
        /// <summary>
        ///     A GUID extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool In(this Guid @this, params Guid[] values) => Array.IndexOf(values, @this) != -1;

        /// <summary>
        ///     A GUID extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn(this Guid @this, params Guid[] values) => Array.IndexOf(values, @this) == -1;

        /// <summary>A GUID extension method that query if '@this' is empty.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty(this Guid @this) => @this == Guid.Empty;

        /// <summary>A GUID extension method that queries if a not is empty.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if a not is empty, false if not.</returns>
        public static bool IsNotEmpty(this Guid @this) => @this != Guid.Empty;
    }
}