using System.Collections.Generic;
using Lucene.Net.Search;
using MultiFacetLucene;
using MultiFacetLucene.Configuration;

namespace Examine.Facets.MultiFacets
{
    public class MultiFacetRequest
    {
        public Query Query { get; set; }

        public int MaxResults { get; set; } 

        public FacetSearcherConfiguration Config { get; set; }

        public List<FacetFieldInfo> Facets { get; set; }
    }
}