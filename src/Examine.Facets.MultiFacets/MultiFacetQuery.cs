using System.Collections.Generic;
using System.Linq;
using Examine.Facets.LuceneEngine;
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

        public MultiFacetQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {
            _searchContext = searchContext;
        }

        public override ISearchResults Execute(int maxResults = 500)
        {
            var request = BuildRequest(maxResults);

            return new MultiFacetSearchResults(_searchContext, request);
        }

        private MultiFacetRequest BuildRequest(int maxResults)
        {
            var request = new MultiFacetRequest
            {
                Query = Query,
                Sort = SortFields.ToList(),
                MaxResults = maxResults,
                Config = FacetSearcherConfiguration.Default(),
                Facets = new List<FacetFieldInfo>()
            };

            foreach (var field in Fields)
            {
                if (field.MinHits > 0)
                {
                    request.Config.MinimumCountInTotalDatasetForFacet = field.MinHits;
                }

                var fieldInfo = new FacetFieldInfo
                {
                    FieldName = field.Name,
                    MaxToFetchExcludingSelections = field.MaxCount
                };

                if (field.Values != null)
                {
                    fieldInfo.Selections = field.Values.ToList();
                }

                request.Facets.Add(fieldInfo);
            }

            return request;
        }
    }
}