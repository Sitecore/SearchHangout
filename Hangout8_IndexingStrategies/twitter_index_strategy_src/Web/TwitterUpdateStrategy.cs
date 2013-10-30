using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Maintenance;
using Sitecore.ContentSearch.Maintenance.Strategies;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;
using Streaminvi;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TweetinCore.Interfaces;
using TweetinCore.Interfaces.TwitterToken;
using TwitterToken;

namespace IndexStrategySandBox
{
    public class TwitterUpdateStrategy : IIndexUpdateStrategy
    {
        private ISearchIndex m_index = null;

        public string HashtagTrigger { get; set; }

        public void Initialize(ISearchIndex searchIndex)
        {
            // Store index
            m_index = searchIndex;

            // Create Twitter token
            var token = new Token(
                ConfigurationManager.AppSettings["accessToken"],
                ConfigurationManager.AppSettings["accessTokenSecret"],
                ConfigurationManager.AppSettings["consumerKey"],
                ConfigurationManager.AppSettings["consumerSecret"]);

            // Start twitter streaming thread
            var task = new Task(() => StartTwitterStream(token));
            task.Start();
        }

        async void StartTwitterStream(IToken token)
        {
            var stream = new FilteredStream();

            CrawlingLog.Log.Info("Init TwitterUpdateStrategy with Hashtag '{0}'".FormatWith(HashtagTrigger));
            Log.Info("Init TwitterUpdateStrategy with Hashtag '{0}'".FormatWith(HashtagTrigger), this);

            stream.AddTrack(HashtagTrigger);

            stream.StreamStarted += (sender, args) => CrawlingLog.Log.Info("Twitter stream started");

            // The following call blocks
            stream.StartStream(token, x => HandleTweet(x));
        }

        void HandleTweet(ITweet tweet)
        {
            if ((from tag in tweet.Hashtags where tag.Text == HashtagTrigger select tag).Any())
            {
                Log.Info("Index rebuild triggered from Twitter by '{0}'".FormatWith(tweet.Creator.Name), this);
                CrawlingLog.Log.Info("Index rebuild triggered from Twitter by '{0}'".FormatWith(tweet.Creator.Name));

                if (IndexCustodian.IsIndexingPaused(m_index) || IndexCustodian.IsRebuilding(m_index))
                {
                    CrawlingLog.Log.Warn("Indexing call muted");
                    Log.Info("Indexing call muted", this);
                    return;
                }

                IndexCustodian.FullRebuild(m_index);
            }
        }
    }
}