using System;

namespace Examine.Facets.Search
{
    public interface IFacetQueryField
    {
        /// <summary>
        /// Minimum number of times a term should appear in a document
        /// </summary>
        IFacetQueryField MinHits(int minHits);

        /// <summary>
        /// Maximum number of terms to return
        /// </summary>
        IFacetQueryField MaxCount(int count);

        IFacetQuery And();
    }
}