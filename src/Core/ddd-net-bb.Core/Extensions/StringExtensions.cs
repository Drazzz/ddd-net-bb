using System;

namespace DDDNETBB.Core
{
    public static class StringExtensions
    {
        /// <summary>
        ///     A String extension method to determine if '@this' string value is null, empty or whitespace.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if null, empty or whitespace, false if not.</returns>
        public static bool IsNullEmptyOrWhitespace(this string @this)
            => string.IsNullOrEmpty(@this) || string.IsNullOrWhiteSpace(@this);
    }
}