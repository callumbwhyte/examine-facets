using System.Collections.Generic;

namespace Examine.Facets.Search
{
    public interface IFacetResults : ISearchResults
    {
        /// <summary>
        /// Facets for the search
        /// </summary>
        IDictionary<string, IFacetResult> Facets { get; }
    }
}