using System;

namespace Examine.Facets.Search
{
    public class FacetField : IFacetField
    {
        public FacetField(string name)
        {
            Name = name;
        }

        public FacetField(string name, string[] values)
        {
            Name = name;
            Values = values;
        }

        public FacetField(string name, string[] values, int minHits = 0, int maxCount = 0, bool expandSelection = false)
        {
            Name = name;
            Values = values;
            MinHits = minHits;
            MaxCount = maxCount;
            ExpandSelection = expandSelection;
        }

        public string Name { get; }

        public string[] Values { get; internal set; }

        public int MinHits { get; internal set; }

        public int MaxCount { get; internal set; }

        public bool ExpandSelection { get; internal set; }
    }
}