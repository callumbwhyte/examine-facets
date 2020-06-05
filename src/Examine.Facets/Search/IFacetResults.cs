using System.Collections.Generic;

namespace Examine.Facets.Search
{
    public interface IFacetResults : ISearchResults
    {
        IDictionary<string, IFacetResult> Facets { get; }
    }
}