using System.Collections.Generic;
using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;

namespace Examine.Facets.LuceneEngine
{
    /// <summary>
    /// Implements facets for <see cref="IQueryExecutor"/> with Lucene
    /// </summary>
    public abstract class FacetSearchQuery : LuceneSearchQuery, IFacetQuery
    {
        public FacetSearchQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {

        }

        public IList<IFacetField> Fields { get; } = new List<IFacetField>();

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field) => FacetInternal(field);

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string value) => FacetInternal(field, new string[] { value });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string[] values) => FacetInternal(field, values);

        /// <summary>
        /// Register <see cref="IFacetField"/> for use within query
        /// </summary>
        protected virtual IFacetQueryField FacetInternal(string field, string[] values = null)
        {
            var facet = new FacetField(field, values);

            Fields.Add(facet);

            return new FacetQueryField(this, facet);
        }

        ///<inheritdoc/>
        protected override LuceneBooleanOperationBase CreateOp() => new FacetBooleanOperation(this);

        ///<inheritdoc/>
        public new abstract ISearchResults Execute(int maxResults = 500);
    }
}