namespace seven.layouts
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using Lucene.Net.Index;
    using Lucene.Net.Search;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Search;

    public partial class QueryBuilder : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, EventArgs e)
        {
            GlobalSetup();

            for (int i = 0; i <= 3; i++)
            {
                LinqTest1.Text = RunWhereLinq().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest1.Text = RunWhereQuery().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest1.Text = RunWhereItemEnumerable().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest1.Text = RunWhereFastQuery().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest1.Text = RunWhereSearchManager().ToString(CultureInfo.InvariantCulture) + "ms";


                LinqTest2.Text = RunWhereLinq2().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest2.Text = RunWhereQuery2().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest2.Text = RunWhereItemEnumerable2().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest2.Text = this.RunWhereFastQuery2().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest2.Text = RunWhereSearchManager2().ToString(CultureInfo.InvariantCulture) + "ms";

                LinqTest3.Text = RunWhereLinq3().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest3.Text = RunWhereQuery3().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest3.Text = RunWhereItemEnumerable3().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest3.Text = this.RunWhereFastQuery3().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest3.Text = RunWhereSearchManager3().ToString(CultureInfo.InvariantCulture) + "ms";

                LinqTest4.Text = RunWhereLinq4().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest4.Text = RunWhereQuery4().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest4.Text = RunWhereItemEnumerable4().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest4.Text = this.RunWhereFastQuery4().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest4.Text = RunWhereSearchManager4().ToString(CultureInfo.InvariantCulture) + "ms";

                LinqTest5.Text = RunWhereLinq5().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest5.Text = RunWhereQuery5().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest5.Text = RunWhereItemEnumerable5().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest5.Text = this.RunWhereFastQuery5().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest5.Text = RunWhereSearchManager5().ToString(CultureInfo.InvariantCulture) + "ms";

                LinqTest6.Text = RunWhereLinq6().ToString(CultureInfo.InvariantCulture) + "ms";
                QueryTest6.Text = RunWhereQuery6().ToString(CultureInfo.InvariantCulture) + "ms";
                ItemTest6.Text = RunWhereItemEnumerable6().ToString(CultureInfo.InvariantCulture) + "ms";
                FastQueryTest6.Text = this.RunWhereFastQuery6().ToString(CultureInfo.InvariantCulture) + "ms";
                SearchTest6.Text = RunWhereSearchManager6().ToString(CultureInfo.InvariantCulture) + "ms";
            }

        }

        public void GlobalSetup()
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                GlobalCount.Text = context.GetQueryable<SearchResultItem>().Count().ToString();
            }

        }

        #region Test 6 - Range Queries

        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager6()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid("{16156647-DE5D-4E6A-A6AE-5B8D4A3A40BE}");
                    TermRangeQuery ftQuery = new TermRangeQuery("__updated", "20090101", "20130505", true, true);
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq6()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var run = context.GetQueryable<SearchResultItem>().Where(item => item.CreatedDate > DateTime.Now.AddDays(-100)).Take(20).ToList();
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery6()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore//*[@__Updated >= '20090101']").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereFastQuery6()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore//*[@__Updated >= '20090101']").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable6()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.RootID).Axes.GetDescendants().Where(item => item.Statistics.Created > DateTime.Now.AddDays((-100))).Take(20);
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion

        #region Test 5 - Get Ancestors

        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager5()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid("{16156647-DE5D-4E6A-A6AE-5B8D4A3A40BE}");
                    TermQuery ftQuery = new TermQuery(new Term("_path", id1));
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq5()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid("{16156647-DE5D-4E6A-A6AE-5B8D4A3A40BE}");
                    var results = context.GetQueryable<ExtendedSearchResultItem>().Where(i => i["_group"] == id1);
                    results.First().GetAncestors<SearchResultItem>(context).Take(20);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery5()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore/system/Settings/Buckets/Search Types/Author/ancestor::*").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereFastQuery5()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore/system/Settings/Buckets/Search Types/Author/ancestor::*").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable5()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem("{16156647-DE5D-4E6A-A6AE-5B8D4A3A40BE}").Axes.GetAncestors().Take(20);
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion

        #region Test 4 - Get Descendants

        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager4()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid(Sitecore.ItemIDs.RootID);
                    TermQuery ftQuery = new TermQuery(new Term("_path", id1));
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq4()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid(Sitecore.ItemIDs.RootID);
                    var results = context.GetQueryable<ExtendedSearchResultItem>().Where(i => i["_group"] == id1);
                    results.First().GetDescendants<SearchResultItem>(context).Take(20);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery4()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore//*").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereFastQuery4()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore//*").Take(20);
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable4()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.RootID).Axes.GetDescendants().Take(20);
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion

        #region Test 3 - Get Children

        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager3()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid(Sitecore.ItemIDs.RootID);
                    TermQuery ftQuery = new TermQuery(new Term("_parent", id1));
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq3()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var id1 = IdHelper.NormalizeGuid(Sitecore.ItemIDs.RootID);
                    var results = context.GetQueryable<ExtendedSearchResultItem>().Where(i => i["_group"] == id1);
                    results.First().GetChildren<SearchResultItem>(context).Where(template => template.TemplateName == "Sample Item");
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery3()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore/*[contains(@@templatename, 'Sample Item')]");
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereFastQuery3()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore/*[@@templatename = 'Sample Item']");
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable3()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.RootID).GetChildren().Where(item => item.TemplateName == "Sample Item");
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion

        #region Test 2 - Contains


        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager2()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    WildcardQuery ftQuery = new WildcardQuery(new Term("_templatename", "*sam*"));
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq2()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var run = context.GetQueryable<SearchResultItem>().Where(item => item.TemplateName.Contains("Sam")).Take(20).ToList();
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery2()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore//*[contains(@@templatename, 'Sam')]");
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereFastQuery2()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore//*[@@templatename = '%Sam%']");
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable2()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.RootID).Axes.GetDescendants().Where(item => item.TemplateName.Contains("Sample Item")).Take(20);
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion

        #region Test 1 - Equals

        public long RunWhereFastQuery()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("fast:/sitecore//*[@@templatename = 'Sample Item']");
                foreach (var item in run)
                {

                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }


        public long RunWhereLinq()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
                {
                    var run = context.GetQueryable<SearchResultItem>().Where(item => item.TemplateName == "Sample Item").Take(20).ToList();
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereQuery()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.SelectItems("/sitecore//*[@@templatename = 'Sample Item']");
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        public long RunWhereItemEnumerable()
        {
            using (var queryTimer = new QueryTimer())
            {
                var run = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.RootID).Axes.GetDescendants().Where(item => item.TemplateName == "Sample Item").Take(20);
                foreach (var item in run)
                {

                }
                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        //NOTE: This required me to bring in the Lucene.Net Reference
        public long RunWhereSearchManager()
        {
            using (var queryTimer = new QueryTimer())
            {
                using (var context = SearchManager.GetIndex("system").CreateSearchContext())
                {
                    TermQuery ftQuery = new TermQuery(new Term("_templatename", "sample item"));
                    SearchHits results = context.Search(ftQuery);
                }

                return queryTimer.StopWatch.ElapsedTicks / 1000;
            }
        }

        #endregion
    }

    public class QueryTimer : IDisposable
    {
        public Stopwatch StopWatch { get; set; }

        public QueryTimer()
        {
            StopWatch = new Stopwatch();
            StopWatch.Start();
        }

        public void Dispose()
        {
            StopWatch.Stop();
        }
    }
}