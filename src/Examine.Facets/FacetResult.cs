using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Examine.Facets
{
    public class FacetResult : IFacetResult
    {
        private readonly IEnumerable<IFacetValue> _values;

        public FacetResult(IEnumerable<IFacetValue> values)
        {
            _values = values;
        }

        ///<inheritdoc/>
        public int GetHits(object value)
        {
            var facet = _values.FirstOrDefault(x => x.Value.Equals(value));

            if (facet == null)
            {
                return 0;
            }

            return facet.Hits;
        }

        public IEnumerator<IFacetValue> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}