using Examine.LuceneEngine;
using Examine.LuceneEngine.Providers;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;
using Lucene.Net.Index;

namespace Examine.Facets.MultiFacets
{
    public class MultiFacetSearcher : LuceneSearcher
    {
        public MultiFacetSearcher(string name, IndexWriter writer, Analyzer analyzer, FieldValueTypeCollection fieldValueTypes)
            : base(name, writer, analyzer, fieldValueTypes)
        {

        }

        public override IQuery CreateQuery(string category = null, BooleanOperation defaultOperation = BooleanOperation.And)
        {
            return new MultiFacetQuery(GetSearchContext(), category, LuceneAnalyzer, GetAllIndexedFields(), new LuceneSearchOptions(), defaultOperation);
        }
    }
}