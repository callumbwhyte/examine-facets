using Examine.Search;

namespace Examine.Facets.Search
{
    /// <summary>
    /// Extends <see cref="IQuery"/> to support facets
    /// </summary>
    public interface IFacetQuery : IQuery
    {
        /// <summary>
        /// Add a facet to the current query
        /// </summary>
        IFacetQueryField Facet(string field);

        /// <summary>
        /// Add a facet to the current query, filtered by value
        /// </summary>
        IFacetQueryField Facet(string field, string value);

        /// <summary>
        /// Add a facet to the current query, filtered by multiple values
        /// </summary>
        IFacetQueryField Facet(string field, string[] values);
    }
}