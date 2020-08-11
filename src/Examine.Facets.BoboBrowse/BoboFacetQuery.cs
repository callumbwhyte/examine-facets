using System.Collections.Generic;
using BoboBrowse.Net;
using BoboBrowse.Net.Facets;
using BoboBrowse.Net.Facets.Impl;
using Examine.Facets.LuceneEngine;
using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;

namespace Examine.Facets.BoboBrowse
{
    internal class BoboFacetQuery : FacetSearchQuery
    {
        private readonly ISearchContext _searchContext;

        private readonly BoboFacetRequest _request;

        public BoboFacetQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {
            _searchContext = searchContext;

            _request = new BoboFacetRequest
            {
                BrowseRequest = new BrowseRequest
                {
                    Query = Query,
                    FetchStoredFields = true
                },
                FacetHandlers = new List<IFacetHandler>()
            };
        }

        protected override void FacetInternal(IFacetField field)
        {
            _request.FacetHandlers.Add(new MultiValueFacetHandler(field.Name));

            _request.BrowseRequest.SetFacetSpec(field.Name, new FacetSpec()
            {
                MinHitCount = field.MinHits,
                MaxCount = field.MaxCount
            });

            if (field.Values != null)
            {
                _request.BrowseRequest.AddSelection(new BrowseSelection(field.Name)
                {
                    Values = field.Values
                });
            }
        }

        public override ISearchResults Execute(int maxResults = 500)
        {
            _request.BrowseRequest.Count = maxResults;
            _request.BrowseRequest.Sort = SortFields.ToArray();

            return new BoboFacetSearchResults(_searchContext, _request);
        }
    }
}