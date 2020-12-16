using System;

namespace Examine.Facets.Search
{
    public class FacetQueryField : IFacetQueryField
    {
        private readonly IFacetQuery _query;
        private readonly FacetField _field;

        public FacetQueryField(IFacetQuery query, FacetField field)
        {
            _query = query;
            _field = field;
        }

        ///<inheritdoc/>
        public IFacetQueryField MinHits(int minHits)
        {
            _field.MinHits = minHits;

            return this;
        }

        ///<inheritdoc/>
        public IFacetQueryField MaxCount(int count)
        {
            _field.MaxCount = count;

            return this;
        }

        ///<inheritdoc/>
        public IFacetQueryField ExpandSelection(bool expandSelection = true)
        {
            _field.ExpandSelection = expandSelection;

            return this;
        }

        public IFacetQuery And() => _query;
    }
}