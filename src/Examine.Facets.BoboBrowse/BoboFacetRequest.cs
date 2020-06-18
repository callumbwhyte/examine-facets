using System.Collections.Generic;
using BoboBrowse.Net;
using BoboBrowse.Net.Facets;

namespace Examine.Facets.BoboBrowse
{
    public class BoboFacetRequest
    {
        public BrowseRequest BrowseRequest { get; set; }

        public List<IFacetHandler> FacetHandlers { get; set; }
    }
}