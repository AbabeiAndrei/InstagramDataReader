using System;
using System.Diagnostics;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram
{
    public class InstagramLikes : InstagramDataElement<IInstagramLike>, IInstagramLikes
    {
        public override string Name => "Likes";

        public virtual string File => "likes.json";

        public virtual IInstagramLikeCollection MediaLikes { get; protected set; }

        public virtual IInstagramLikeCollection CommentLikes { get; protected set; }

        protected override IInstagramLike CreateEntity(JToken jToken)
        {
            return new InstagramLike
            {
                Date = jToken[0].Value<DateTime>(),
                Like = jToken[1].Value<string>()
            };
        }

        internal override void Load(JObject jObject)
        {
            MediaLikes = CreateCollection(jObject, new InstagramLikeCollection("Media Likes"), "media_likes");
            CommentLikes = CreateCollection(jObject, new InstagramLikeCollection("Comment Likes"), "comment_likes");
        }

        protected override IEnumerable<INode> GetChilds()
        {
            yield return MediaLikes;
            yield return CommentLikes;
        }
    }

    public class InstagramLikeCollection : BaseInstagramCollectionElements<IInstagramLike>, IInstagramLikeCollection
    {
        public override string Name { get; }

        protected override IList<IInstagramLike> Collection { get; }

        private InstagramLikeCollection()
        {
            Collection = new List<IInstagramLike>();
        }

        public InstagramLikeCollection(string name)
            : this()
        {
            Name = name;
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<IInstagramLike>());
        }
    }

    [DebuggerDisplay("Date: {Date}, Like: {Like}")]
    public class InstagramLike : IInstagramLike
    {
        public DateTime Date { get; set; }

        public string Like { get; set; }
    }
}
