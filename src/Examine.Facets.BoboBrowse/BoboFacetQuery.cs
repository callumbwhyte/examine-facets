using System.Collections.Generic;
using System.Linq;
using BoboBrowse.Net;
using BoboBrowse.Net.Facets;
using BoboBrowse.Net.Facets.Impl;
using Examine.Facets.LuceneEngine;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;

namespace Examine.Facets.BoboBrowse
{
    internal class BoboFacetQuery : FacetSearchQuery
    {
        private readonly ISearchContext _searchContext;

        public BoboFacetQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {
            _searchContext = searchContext;
        }

        public override ISearchResults Execute(int maxResults = 500)
        {
            var request = BuildRequest(maxResults);

            return new BoboFacetSearchResults(_searchContext, request);
        }

        private BoboFacetRequest BuildRequest(int maxResults)
        {
            var request = new BoboFacetRequest
            {
                BrowseRequest = new BrowseRequest
                {
                    Query = Query,
                    Sort = SortFields.ToArray(),
                    Count = maxResults,
                    FetchStoredFields = true
                },
                FacetHandlers = new List<IFacetHandler>()
            };

            foreach (var field in Fields)
            {
                var spec = new FacetSpec()
                {
                    MinHitCount = field.MinHits,
                    MaxCount = field.MaxCount
                };

                request.BrowseRequest.SetFacetSpec(field.Name, spec);

                if (field.Values != null)
                {
                    request.BrowseRequest.AddSelection(new BrowseSelection(field.Name)
                    {
                        Values = field.Values
                    });
                }

                request.FacetHandlers.Add(new MultiValueFacetHandler(field.Name));
            }

            return request;
        }
    }
}