using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Examine.Facets.Search
{
    public class FacetSearchResultsBase : ISearchResults, IFacetResults
    {
        public FacetSearchResultsBase()
        {
            Results = new Dictionary<int, ISearchResult>();
            Facets = new Dictionary<string, IFacetResult>();
        }

        /// <summary>
        /// Results for the search
        /// </summary>
        public IDictionary<int, ISearchResult> Results { get; }

        /// <summary>
        /// Facets for the search
        /// </summary>
        public IDictionary<string, IFacetResult> Facets { get; }

        /// <summary>
        /// Total number of results for the search
        /// </summary>
        public long TotalItemCount { get; set; }

        /// <summary>
        /// Skip to a particular point in the results
        /// </summary>
        public IEnumerable<ISearchResult> Skip(int skip)
        {
            return Results.Values.Skip(skip);
        }

        /// <summary>
        /// Gets the enumerator for the results
        /// </summary>
        public IEnumerator<ISearchResult> GetEnumerator()
        {
            return Skip(0).GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator for the results
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}