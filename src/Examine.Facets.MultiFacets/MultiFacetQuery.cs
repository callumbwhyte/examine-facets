using System.Collections.Generic;
using System.Linq;
using Examine.Facets.LuceneEngine;
using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;
using MultiFacetLucene;
using MultiFacetLucene.Configuration;

namespace Examine.Facets.MultiFacets
{
    internal class MultiFacetQuery : FacetSearchQuery
    {
        private readonly ISearchContext _searchContext;

        private readonly MultiFacetRequest _request;

        public MultiFacetQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {
            _searchContext = searchContext;

            _request = new MultiFacetRequest
            {
                Query = Query,
                Config = FacetSearcherConfiguration.Default(),
                Facets = new List<FacetFieldInfo>()
            };
        }

        protected override void FacetInternal(IFacetField field)
        {
            if (field.MinHits > 0)
            {
                _request.Config.MinimumCountInTotalDatasetForFacet = field.MinHits;
            }

            var facet = new FacetFieldInfo
            {
                FieldName = field.Name,
                MaxToFetchExcludingSelections = field.MaxCount
            };

            if (field.Values != null)
            {
                facet.Selections = field.Values.ToList();
            }

            _request.Facets.Add(facet);
        }

        public override ISearchResults Execute(int maxResults = 500)
        {
            _request.MaxResults = maxResults;

            return new MultiFacetSearchResults(_searchContext, _request);
        }
    }
}