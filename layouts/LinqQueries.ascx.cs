using System;
using System.Linq;

namespace seven.layouts
{
    using System.Globalization;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;

    public partial class LinqQueries : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Example1();
            this.Example2();
            this.Example3();
            this.Example4();
            this.Example5();
        }

        public void Example1()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                var query = context.GetQueryable<Article>().Where(item => item.Title == "News Article");
                GridView1.DataSource = query;
                GridView1.DataBind();
            }
        }

        public void Example2()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                var query = context.GetQueryable<Article>().Where(item => item.Title.Contains("News"));
                GridView2.DataSource = query;
                GridView2.DataBind();
            }
        }

        public void Example3()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                var query = context.GetQueryable<Article>().Where(item => item.PublishDate.Between(DateTime.Now.AddDays(-10), DateTime.Now, Inclusion.Both));
                GridView3.DataSource = query;
                GridView3.DataBind();
            }
        }

        public void Example4()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                var query = context.GetQueryable<Article>().Where(item => item.Title.StartsWith("New")).Select(projection => new { Name = projection.Title, PubDate = projection.PublishDate});
                GridView4.DataSource = query;
                GridView4.DataBind();
            }
        }

        public void Example5()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                var query = context.GetQueryable<Article>().Where(item => item.Title.StartsWith("New")).FacetOn(i => i.PublishDate).GetResults();
                GridView5.DataSource = query.Hits;
                GridView5.DataBind();

                foreach (var facet in query.Facets.Categories)
                {
                    Facets.Text = Facets.Text + facet.Name + " : " + facet.Values.Count;
                }

                Count.Text = query.TotalSearchResults.ToString(CultureInfo.InvariantCulture);

            }
        }
    }
}