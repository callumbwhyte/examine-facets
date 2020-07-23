using System.Collections.Generic;
using System.Linq;
using Examine.Facets.LuceneEngine;
using Examine.LuceneEngine.Search;
using Lucene.Net.Search;
using MultiFacetLucene;

namespace Examine.Facets.MultiFacets
{
    internal class MultiFacetSearchResults : FacetSearchResults
    {
        private ScoreDoc[] _scoreDocs;

        public MultiFacetSearchResults(ISearchContext searchContext, MultiFacetRequest request)
            : base(searchContext)
        {
            PerformSearch(request);
        }

        public void PerformSearch(MultiFacetRequest request)
        {
            using (var multiSearcher = new FacetSearcher(Searcher.IndexReader, request.Config))
            {
                var results = multiSearcher.SearchWithFacets(request.Query, request.MaxResults, request.Facets);

                _scoreDocs = results.Hits.ScoreDocs;

                foreach (var facet in results.Facets.GroupBy(x => x.FacetFieldName))
                {
                    Facets.Add(facet.Key, CreateFacetResult(facet.ToList()));
                }

                TotalItemCount = results.Hits.TotalHits;
            }
        }

        protected override int GetTotalDocs() => _scoreDocs.Length;

        protected override ISearchResult GetSearchResult(int index)
        {
            var scoreDoc = _scoreDocs[index];

            if (scoreDoc == null)
            {
                return null;
            }

            var doc = Searcher.Doc(scoreDoc.Doc);

            if (doc == null)
            {
                return null;
            }

            return CreateSearchResult(doc, scoreDoc.Score);
        }

        private IFacetResult CreateFacetResult(IEnumerable<FacetMatch> facets)
        {
            var values = facets.Select(x => new FacetValue(x.Value, (int)x.Count));

            return new FacetResult(values);
        }
    }
}