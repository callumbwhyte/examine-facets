using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Search;

namespace Examine.Facets.LuceneEngine
{
    /// <summary>
    /// Implements facets for <see cref="IBooleanOperation"/> with Lucene
    /// </summary>
    internal class FacetBooleanOperation : LuceneBooleanOperation
    {
        private readonly FacetSearchQuery _search;

        public FacetBooleanOperation(FacetSearchQuery search)
            : base(search)
        {
            _search = search;
        }

        public override ISearchResults Execute(int maxResults = 500) => _search.Execute(maxResults);

        public override IQuery And() => new FacetQuery(_search, Occur.MUST);

        public override IQuery Or() => new FacetQuery(_search, Occur.SHOULD);

        public override IQuery Not() => new FacetQuery(_search, Occur.MUST_NOT);

        protected override INestedQuery AndNested() => new FacetQuery(_search, Occur.MUST);

        protected override INestedQuery OrNested() => new FacetQuery(_search, Occur.SHOULD);

        protected override INestedQuery NotNested() => new FacetQuery(_search, Occur.MUST_NOT);
    }
}