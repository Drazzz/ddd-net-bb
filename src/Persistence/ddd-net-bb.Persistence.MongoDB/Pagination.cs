using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Core.Abstractions.Pagination;
using DDDNETBB.Core.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DDDNETBB.Persistence.MongoDB
{
    public static class Pagination
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 20;
        private const string DefaultSort = "asc";


        public static async Task<PagedResult<T>> Paginate<T>(this IMongoQueryable<T> collection, IPagedQuery query, CancellationToken cancellationToken = default)
            => await collection.Paginate(query.OrderBy, query.SortOrder, query.Page, query.Results, cancellationToken);

        public static async Task<PagedResult<T>> Paginate<T>(this IMongoQueryable<T> collection,
            string orderBy, SortOrder sortOrder, int page = DefaultPage, int resultsPerPage = DefaultPageSize, CancellationToken cancellationToken = default)
            => await collection.Paginate(orderBy, sortOrder?.Name, page, resultsPerPage, cancellationToken);
            
        public static async Task<PagedResult<T>> Paginate<T>(this IMongoQueryable<T> collection, string orderBy,
            string sortOrderDirection = DefaultSort, int page = DefaultPage, int resultsPerPage = DefaultPageSize, CancellationToken cancellationToken = default)
        {
            if (page <= 0) page = 1;
            if (resultsPerPage <= 0) resultsPerPage = 10;

            var isEmpty = await collection.AnyAsync(cancellationToken) == false;
            if (isEmpty)
                return PagedResult<T>.Empty;

            var totalResults = await collection.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);

            List<T> data;
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                data = await collection.Limit(page, resultsPerPage).ToListAsync(cancellationToken);
                return PagedResult<T>.From(data, page, resultsPerPage, totalPages, totalResults);
            }
            
            var sortOrder = SortOrder.From(sortOrderDirection);
            if (sortOrder == SortOrder.Ascending)
                data = await collection.OrderBy(ToLambda<T>(orderBy)).Limit(page, resultsPerPage).ToListAsync(cancellationToken);
            else
                data = await collection.OrderByDescending(ToLambda<T>(orderBy)).Limit(page, resultsPerPage).ToListAsync(cancellationToken);

            return PagedResult<T>.From(data, page, resultsPerPage, totalPages, totalResults);
        }
        public static IMongoQueryable<T> Limit<T>(this IMongoQueryable<T> collection, IPagedQuery query)
            => collection.Limit(query.Page, query.Results);

        public static IMongoQueryable<T> Limit<T>(this IMongoQueryable<T> collection,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0) page = 1;
            if (resultsPerPage <= 0) resultsPerPage = 10;
            
            var skip = (page - 1) * resultsPerPage;
            var data = collection.Skip(skip)
                .Take(resultsPerPage);

            return data;
        }


        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}