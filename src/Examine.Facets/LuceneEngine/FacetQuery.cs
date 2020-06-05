using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Search;

namespace Examine.Facets.LuceneEngine
{
    /// <summary>
    /// Implements facets for <see cref="IQuery"/> with Lucene
    /// </summary>
    internal class FacetQuery : LuceneQuery, IFacetQuery
    {
        private readonly FacetSearchQuery _search;

        public FacetQuery(FacetSearchQuery search, Occur occurrence)
            : base(search, occurrence)
        {
            _search = search;
        }

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field) => _search.Facet(field);

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string value) => _search.Facet(field, value);

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string[] values) => _search.Facet(field, values);
    }
}