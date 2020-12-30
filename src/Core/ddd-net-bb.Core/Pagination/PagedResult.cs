using System;
using System.Collections.Generic;
using System.Linq;
using DDDNETBB.Core.Abstractions.Pagination;
using Newtonsoft.Json;

namespace DDDNETBB.Core.Pagination
{
    public class PagedResult<T> : PagedResultBase
    {
        public IReadOnlyCollection<T> Items { get; }

        public bool IsEmpty => Items is null || !Items.Any();
        public bool IsNotEmpty => !IsEmpty;


        protected PagedResult()
            => Items = Enumerable.Empty<T>().ToList();
        [JsonConstructor]
        protected PagedResult(IEnumerable<T> items, int currentPage, int resultsCountPerPage, int totalPages, long totalResultsCount)
            : base(currentPage, resultsCountPerPage, totalPages, totalResultsCount)
                => Items = items?.ToList()?.AsReadOnly() ?? throw new ArgumentNullException(nameof(items));


        public static PagedResult<T> From(IEnumerable<T> items, int currentPage, int resultsCountPerPage, int totalPages, long totalResultsCount)
            => new(items, currentPage, resultsCountPerPage, totalPages, totalResultsCount);

        public static PagedResult<T> From(PagedResultBase result, IEnumerable<T> items)
            => new(items, result.CurrentPage, result.ResultsPerPage,
                result.TotalPages, result.TotalResults);

        public static PagedResult<T> Empty => new();

        public PagedResult<U> Map<U>(Func<T, U> map) => PagedResult<U>.From(this, Items.Select(map));
    }
}