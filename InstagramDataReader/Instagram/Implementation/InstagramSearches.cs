using System;
using System.Diagnostics;
using System.Collections.Generic;

using InstagramDataReader.Interfaces;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramDataReader.Instagram
{
    public class InstagramSearches : BaseInstagramCollectionElements<IInstagramSearch>, IInstagramSearches
    {
        public override string Name => "Searches";

        public virtual string File => "searches.json";

        protected override IList<IInstagramSearch> Collection { get; }

        public InstagramSearches()
        {
            Collection = new List<IInstagramSearch>();
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<InstagramSearch>());
        }
    }

    [DebuggerDisplay("Search: {Search}, Date: {Date}")]
    public class InstagramSearch : IInstagramSearch
    {
        [JsonProperty("search_click")]
        public string Search { get; set; }

        [JsonProperty("time")]
        public DateTime Date { get; set; }

        public string Type { get; set; }
    }
}
