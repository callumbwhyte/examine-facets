using System;
using Examine.Facets.Search;
using Examine.Search;

namespace Examine.Facets
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Add a facet to the current query
        /// </summary>
        public static IFacetQueryField Facet(this IQuery query, string field)
        {
            if (!(query is IFacetQuery facetQuery))
            {
                throw new Exception("Searcher does not support facets");
            }

            return facetQuery.Facet(field);
        }

        /// <summary>
        /// Add a facet to the current query, filtered by value
        /// </summary>
        public static IFacetQueryField Facet(this IQuery query, string field, string value)
        {
            if (!(query is IFacetQuery facetQuery))
            {
                throw new Exception("Searcher does not support facets");
            }

            return facetQuery.Facet(field, value);
        }

        /// <summary>
        /// Add a facet to the current query, filtered by multiple values
        /// </summary>
        public static IFacetQueryField Facet(this IQuery query, string field, string[] values)
        {
            if (!(query is IFacetQuery facetQuery))
            {
                throw new Exception("Searcher does not support facets");
            }

            return facetQuery.Facet(field, values);
        }
    }
}