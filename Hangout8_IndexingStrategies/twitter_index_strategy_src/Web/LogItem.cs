using Sitecore.ContentSearch.Diagnostics;
using Sitecore.Data;
using Sitecore.Events;
using System;
using Sitecore.StringExtensions;
using Sitecore.ContentSearch;
using Sitecore.Diagnostics;
using Sitecore.Data.Events;
using Sitecore.Data.Items;

namespace IndexStrategySandBox
{
    public class LogItem
    {
        private const string Prefix = "--IndexDemo--";

        public void ItemUpdated(object sender, EventArgs args)
        {
            var uri = Event.ExtractParameter<SitecoreItemUniqueId>(args, 1);
            var item = Database.GetItem(uri);
            Log.Info("{0} Item updated in index: '{1}' ({2})".FormatWith(Prefix, item.Name, item.Uri), this);
        }

        public void ItemCreated(object sender, EventArgs args)
        {
            var targs = Event.ExtractParameter<ItemCreatedEventArgs>(args, 0);
            Log.Info("{0} Item created: {1} ({2})".FormatWith(Prefix, targs.Item.Name, targs.Item.Uri), this);
        }

        public void ItemSaved(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter<Item>(args, 0);
            Log.Info("{0} Item saved: {1} ({2})".FormatWith(Prefix, item.Name, item.Uri), this);
        }
    }
}