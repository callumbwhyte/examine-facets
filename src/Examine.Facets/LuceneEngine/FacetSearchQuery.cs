using System.Collections.Generic;
using Examine.Facets.Search;
using Examine.LuceneEngine.Search;
using Examine.Search;
using Lucene.Net.Analysis;
using Lucene.Net.Search;

namespace Examine.Facets.LuceneEngine
{
    /// <summary>
    /// Implements facets for <see cref="IQueryExecutor"/> with Lucene
    /// </summary>
    public abstract class FacetSearchQuery : LuceneSearchQuery, IFacetQuery
    {
        private readonly ISearchContext _searchContext;

        public List<SortField> SortFields { get; }

        public FacetSearchQuery(ISearchContext searchContext, string category, Analyzer analyzer, string[] fields, LuceneSearchOptions searchOptions, BooleanOperation occurance)
            : base(searchContext, category, analyzer, fields, searchOptions, occurance)
        {
            _searchContext = searchContext;

            SortFields = new List<SortField>();
        }

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field) => Facet(field, new string[] { });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string value) => Facet(field, new string[] { value });

        ///<inheritdoc/>
        public IFacetQueryField Facet(string field, string[] values)
        {
            var facet = new FacetField(field)
            {
                Values = values
            };

            FacetInternal(facet);

            return new FacetQueryField(this, facet);
        }

        /// <summary>
        /// Register <see cref="IFacetField"/> for use within query
        /// </summary>
        protected abstract void FacetInternal(IFacetField field);

        ///<inheritdoc/>
        public new IBooleanOperation OrderBy(params SortableField[] fields)
        {
            OrderByInternal(false, fields);

            return base.OrderBy(fields);
        }

        ///<inheritdoc/>
        public new IBooleanOperation OrderByDescending(params SortableField[] fields)
        {
            OrderByInternal(true, fields);

            return base.OrderBy(fields);
        }

        /// <summary>
        /// Register <see cref="SortableField"/> for use within query
        /// </summary>
        protected virtual void OrderByInternal(bool descending, params SortableField[] fields)
        {
            foreach (var field in fields)
            {
                var defaultSort = SortField.STRING;

                switch (field.SortType)
                {
                    case SortType.Score:
                        defaultSort = SortField.SCORE;
                        break;
                    case SortType.DocumentOrder:
                        defaultSort = SortField.DOC;
                        break;
                    case SortType.String:
                        defaultSort = SortField.STRING;
                        break;
                    case SortType.Int:
                        defaultSort = SortField.INT;
                        break;
                    case SortType.Float:
                        defaultSort = SortField.FLOAT;
                        break;
                    case SortType.Long:
                        defaultSort = SortField.LONG;
                        break;
                    case SortType.Double:
                        defaultSort = SortField.DOUBLE;
                        break;
                    case SortType.Short:
                        defaultSort = SortField.SHORT;
                        break;
                    case SortType.Byte:
                        defaultSort = SortField.BYTE;
                        break;
                }

                var fieldName = field.FieldName;

                var valueType = _searchContext.GetFieldValueType(fieldName);

                if (valueType?.SortableFieldName != null)
                {
                    fieldName = valueType.SortableFieldName;
                }

                SortFields.Add(new SortField(fieldName, defaultSort, descending));
            }
        }

        protected override LuceneBooleanOperationBase CreateOp() => new FacetBooleanOperation(this);

        ///<inheritdoc/>
        public new abstract ISearchResults Execute(int maxResults = 500);
    }
}