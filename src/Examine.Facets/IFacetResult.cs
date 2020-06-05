using System.Collections.Generic;

namespace Examine.Facets
{
    public interface IFacetResult : IEnumerable<IFacetValue>
    {
        /// <summary>
        /// Gets the number of times a term occurs
        /// </summary>
        int GetHits(object value);
    }
}