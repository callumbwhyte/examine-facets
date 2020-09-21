using System;
using System.Collections.Generic;
using Examine.Facets.Search;
using Examine.LuceneEngine;
using Examine.LuceneEngine.Search;
using Lucene.Net.Search;

namespace Examine.Facets.LuceneEngine
{
    public abstract class FacetSearchResults : LuceneSearchResultsBase, IFacetResults
    {
        public FacetSearchResults(ISearchContext searchContext)
        {
            Facets = new Dictionary<string, IFacetResult>();

            if (!(searchContext.Searcher is IndexSearcher indexSearcher))
            {
                throw new Exception("Searcher is invalid");
            }

            Searcher = indexSearcher;
        }

        ///<inheritdoc/>
        public IDictionary<string, IFacetResult> Facets { get; }

        /// <summary>
        /// Exposes the internal <see cref="IndexSearcher"/>
        /// </summary>
        public IndexSearcher Searcher { get; }
    }
}