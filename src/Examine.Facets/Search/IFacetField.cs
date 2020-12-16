using System;

namespace Examine.Facets.Search
{
    public interface IFacetField
    {
        string Name { get; }
        string[] Values { get; }
        int MinHits { get; }
        int MaxCount { get; }
        bool ExpandSelection { get; }
    }
}