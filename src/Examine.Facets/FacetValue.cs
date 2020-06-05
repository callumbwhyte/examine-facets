using System;

namespace Examine.Facets
{
    public class FacetValue : IFacetValue
    {
        public FacetValue(object value, int hits)
        {
            Value = value;
            Hits = hits;
        }

        public object Value { get; }

        public int Hits { get; }
    }
}