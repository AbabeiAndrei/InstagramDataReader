using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram
{
    public class InstagramSettings : InstagramDataElement<IInstagramSettings>, IInstagramSettings
    {
        public override string Name => "Settings";

        public virtual string File => "settings.json";

        [JsonProperty("allow_comments_from")]
        public virtual string AllowCommentsFrom { get; protected set; }

        [JsonProperty("blocked_commenters")]
        public virtual IEnumerable<string> BlockedCommenters { get; protected set; }

        [JsonProperty("filtered_keywords")]
        public virtual IEnumerable<string> FilteredKeywords { get; protected set; }

        protected override IInstagramSettings CreateEntity(JToken jToken)
        {
            return jToken.ToObject<InstagramSettings>();
        }

        internal override void Load(JObject jObject)
        {
            var entity = CreateEntity(jObject.Root);

            AllowCommentsFrom = entity.AllowCommentsFrom;
            BlockedCommenters = entity.BlockedCommenters;
            FilteredKeywords = entity.FilteredKeywords;
        }
    }
}
