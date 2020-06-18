using System.Linq;
using BoboBrowse.Net;
using Examine.Facets.LuceneEngine;
using Examine.LuceneEngine.Search;

namespace Examine.Facets.BoboBrowse
{
    internal class BoboFacetSearchResults : FacetSearchResults
    {
        private BrowseHit[] _browseHits;

        public BoboFacetSearchResults(ISearchContext searchContext, BoboFacetRequest request)
            : base(searchContext)
        {
            PerformSearch(request);
        }

        public void PerformSearch(BoboFacetRequest request)
        {
            using (var boboReader = BoboIndexReader.GetInstance(Searcher.IndexReader, request.FacetHandlers))
            using (var browser = new BoboBrowser(boboReader))
            {
                var results = browser.Browse(request.BrowseRequest);

                _browseHits = results.Hits;

                foreach (var facet in results.FacetMap)
                {
                    Facets.Add(facet.Key, CreateFacetResult(facet.Value));
                }

                TotalItemCount = results.NumHits;
            }
        }

        protected override int GetTotalDocs() => _browseHits.Length;

        protected override ISearchResult GetSearchResult(int index)
        {
            var hit = _browseHits[index];

            if (hit == null)
            {
                return null;
            }

            return CreateSearchResult(hit.StoredFields, hit.Score);
        }

        private IFacetResult CreateFacetResult(IFacetAccessible facet)
        {
            var values = facet
                .GetFacets()
                .Select(x => new FacetValue(x.Value, x.FacetValueHitCount));

            return new FacetResult(values);
        }
    }
}