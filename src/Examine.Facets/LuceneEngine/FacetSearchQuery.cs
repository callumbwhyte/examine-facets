using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;

namespace Examine.Facets.LuceneEngine
{
    /// <summary>
    /// Implements facets for <see cref="IQueryExecutor"/> with Lucene
    /// </summary>
    public class FacetSearchQuery : LuceneSearchQuery, IFacetQuery
    {
        public FacetSearchQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {

        }

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field) => Facet(field, new string[] { });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string value) => Facet(field, new string[] { value });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string[] values)
        {
            var facet = new FacetField(field)
            {
                Values = values
            };

            return new FacetQueryField(this, facet);
        }

        protected override LuceneBooleanOperationBase CreateOp() => new FacetBooleanOperation(this);
    }
}