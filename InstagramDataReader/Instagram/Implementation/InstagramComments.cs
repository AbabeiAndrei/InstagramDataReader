using System;
using System.Diagnostics;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;
using System.Windows.Forms;

using InstagramDataReader.Interfaces;
using InstagramDataReader.Instagram.Controls;

namespace InstagramDataReader.Instagram
{
    public class InstagramComments : InstagramDataElement<IInstagramComment>, IInstagramComments
    {
        public virtual string File => "comments.json";

        public override string Name => "Comments";

        public virtual IInstagramCommentCollection MediaComments { get; private set; }

        public virtual IInstagramCommentCollection LiveComments { get; private set; }

        internal override void Load(JObject jObject)
        {
            MediaComments = CreateCollection(jObject, new InstagramCommentCollection("Media Comments"), "media_comments");
            LiveComments = CreateCollection(jObject, new InstagramCommentCollection("Live Comments"), "live_comments");
        }

        protected override IInstagramComment CreateEntity(JToken jToken)
        {
            return new InstagramComment
            {
                Date = jToken[0].Value<DateTime>(),
                Comment = jToken[1].Value<string>(),
                User = jToken[2].Value<string>()
            };
        }

        protected override IEnumerable<INode> GetChilds()
        {
            yield return MediaComments;
            yield return LiveComments;
        }
    }

    public class InstagramCommentCollection : BaseInstagramCollectionElements<IInstagramComment>, IInstagramCommentCollection, INodeControl
    {
        public override string Name { get; }

        protected override IList<IInstagramComment> Collection { get; }

        private InstagramCommentCollection()
        {
            Collection = new List<IInstagramComment>();
        }

        public InstagramCommentCollection(string name)
            : this()
        {
            Name = name;
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<InstagramComment>());
        }

        public Control CreateControl()
        {
            return new InstragramCommentsControl(this);
        }
    }

    [DebuggerDisplay("Date : {Date}, User : {User}, Comment : {Comment}")]
    public class InstagramComment : IInstagramComment
    {
        public DateTime Date { get; set; }

        public string User { get; set; }

        public string Comment { get; set; }
    }
}