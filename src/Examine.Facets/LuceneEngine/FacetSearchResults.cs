using System;
using System.Collections.Generic;
using System.Linq;
using Examine.Facets.Search;
using Examine.LuceneEngine.Providers;
using Examine.LuceneEngine.Search;
using Lucene.Net.Documents;
using Lucene.Net.Search;

namespace Examine.Facets.LuceneEngine
{
    public abstract class FacetSearchResults : FacetSearchResultsBase
    {
        public FacetSearchResults(ISearchContext searchContext)
        {
            if (!(searchContext.Searcher is IndexSearcher indexSearcher))
            {
                throw new Exception("Searcher is invalid");
            }

            Searcher = indexSearcher;
        }

        /// <summary>
        /// Exposes the internal <see cref="IndexSearcher"/>
        /// </summary>
        public IndexSearcher Searcher { get; }

        /// <summary>
        /// Creates an <see cref="ISearchResult"/> for a given Lucene <see cref="Document"/>
        /// </summary>
        protected virtual ISearchResult CreateSearchResult(Document document, float score)
        {
            var id = document.Get("id");

            if (string.IsNullOrEmpty(id) == true)
            {
                id = document.Get(LuceneIndex.ItemIdFieldName);
            }

            var result = new SearchResult(id, score, () =>
            {
                var fields = document.GetFields();

                var resultVals = new Dictionary<string, List<string>>();

                foreach (var field in fields.Cast<Field>())
                {
                    var fieldName = field.Name;

                    var values = document.GetValues(fieldName);

                    if (resultVals.TryGetValue(fieldName, out var resultFieldVals))
                    {
                        foreach (var value in values)
                        {
                            if (!resultFieldVals.Contains(value))
                            {
                                resultFieldVals.Add(value);
                            }
                        }
                    }
                    else
                    {
                        resultVals[fieldName] = values.ToList();
                    }
                }

                return resultVals;
            });

            return result;
        }
    }
}