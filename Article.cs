using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seven
{
    using Sitecore.ContentSearch;

    public class Article
    {
        public string Title { get; set; }

        [IndexField("__smallcreateddate")]
        public DateTime PublishDate { get; set; }
    }
}