using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seven
{
    using System.Globalization;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Data.Items;

    public class ExtendedSearchResultItem : SearchResultItem
    {
        /// <summary>Gets the Descendants of the result</summary>
        /// <returns>IQueryable<TResult></returns>
        public IQueryable<TResult> GetDescendants<TResult>(IProviderSearchContext context) where TResult : SearchResultItem, new()
        {
            Item sitecoreItem = this.GetItem();
            var s = IdHelper.NormalizeGuid(sitecoreItem.ID.ToString());
            var query = context.GetQueryable<TResult>(new CultureExecutionContext(new CultureInfo(this.Language))).Where(i => (i.Parent == sitecoreItem.ID || i.Paths.Contains(sitecoreItem.ID)) && (i["_group"] != s));
            return query;
        }

        /// <summary>Gets the Children of the result</summary>
        /// <returns>IQueryable<TResult></returns>
        public IQueryable<TResult> GetChildren<TResult>(IProviderSearchContext context) where TResult : SearchResultItem, new()
        {
            Item sitecoreItem = this.GetItem();
            var query = context.GetQueryable<TResult>(new CultureExecutionContext(new CultureInfo(this.Language))).Where(i => i.Parent == sitecoreItem.ID);
            return query;
        }

        /// <summary>Gets the Ancestors of the result</summary>
        /// <returns>IQueryable<TResult></returns>
        public IQueryable<TResult> GetAncestors<TResult>(IProviderSearchContext context) where TResult : SearchResultItem, new()
        {
            var queryBuilder = PredicateBuilder.True<TResult>();
            Item sitecoreItem = this.GetItem();
            var s1 = sitecoreItem.ID;
            var ancestors = this.Paths.RemoveWhere(item => item == s1);
            foreach (var path in ancestors)
            {
                var normalizeGuid = IdHelper.NormalizeGuid(path.ToString());
                queryBuilder = queryBuilder.Or(i => i["_group"] == normalizeGuid);
            }

            var query = context.GetQueryable<TResult>(new CultureExecutionContext(new CultureInfo(this.Language))).Where(queryBuilder);
            return query;
        }

    }
}