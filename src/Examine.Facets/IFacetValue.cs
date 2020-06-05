using System;

namespace Examine.Facets
{
    public interface IFacetValue
    {
        object Value { get; }
        int Hits { get; }
    }
}