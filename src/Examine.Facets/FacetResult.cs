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
		public int GetHits(object value) => _values.FirstOrDefault(x => Equals(x.Value, value))?.Hits ?? 0;

		public IEnumerator<IFacetValue> GetEnumerator() => _values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}