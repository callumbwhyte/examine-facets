using System.Collections;
using System.Collections.Generic;

namespace Examine.Facets.Search
{
    public abstract class FacetSearchResultsBase : ISearchResults, IFacetResults
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
        /// Gets the total number of documents while enumerating results
        /// </summary>
        protected abstract int GetTotalDocs();

        /// <summary>
        /// Gets a result at a given index while enumerating results
        /// </summary>
        protected abstract ISearchResult GetSearchResult(int index);

        /// <summary>
        /// Skip to a particular point in the results
        /// </summary>
        public virtual IEnumerable<ISearchResult> Skip(int skip)
        {
            for (int i = skip, x = GetTotalDocs(); i < x; i++)
            {
                if (Results.TryGetValue(i, out ISearchResult result) == false)
                {
                    result = GetSearchResult(i);

                    if (result == null)
                    {
                        continue;
                    }

                    Results.Add(i, result);
                }

                yield return result;
            }
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