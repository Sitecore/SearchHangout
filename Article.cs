using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seven
{
    using Sitecore.ContentSearch;
    using Sitecore.Data;

    public class Article //: SearchResultItem
    {
        public string Title { get; set; }

        [IndexField("__smallcreateddate")]
        public DateTime PublishDate { get; set; }

        [IndexField(BuiltinFields.DataSource)]
        public string Datasource { get; set; }

        [IndexField(BuiltinFields.ID)]
        public ID ArticleId { get; set; }

    }
}