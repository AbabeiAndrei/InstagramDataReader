using System;
using System.Diagnostics;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;
using System.Windows.Forms;
using InstagramDataReader.Instagram.Controls;

namespace InstagramDataReader.Instagram
{
    public class InstagramConnections : InstagramDataElement<IInstagramConnection>, IInstagramConnections
    {
        public override string Name => "Connections";

        public virtual string File => "connections.json";

        public virtual IInstagramConnectionCollection BlockedUsers { get; private set; }

        public virtual IInstagramConnectionCollection CloseFriends { get; private set; }

        public virtual IInstagramConnectionCollection FollowRequestsSent { get; private set; }

        public virtual IInstagramConnectionCollection Followers { get; private set; }

        public virtual IInstagramConnectionCollection Following { get; private set; }

        public virtual IInstagramConnectionCollection Hashtags { get; private set; }

        protected override IInstagramConnection CreateEntity(JToken jToken)
        {
            var property = (JProperty)jToken;

            return new InstagramConnection(property.Name, property.Value.Value<DateTime>());
        }

        internal override void Load(JObject jObject)
        {
            BlockedUsers = CreateCollection(jObject, new InstagramConnectionCollection("Blocked Users"), "blocked_users");
            CloseFriends = CreateCollection(jObject, new InstagramConnectionCollection("Close Friends"), "close_friends");
            FollowRequestsSent = CreateCollection(jObject, new InstagramConnectionCollection("Follow requests sent"), "follow_requests_sent");
            Followers = CreateCollection(jObject, new InstagramConnectionCollection("Followers"), "followers");
            Following = CreateCollection(jObject, new InstagramConnectionCollection("Following"), "following");
            Hashtags = CreateCollection(jObject, new InstagramConnectionCollection("Following Hashtags"), "following_hashtags");
        }

        protected override IEnumerable<IInstagramConnection> ReadEntities(JObject jObject, string node)
        {
            foreach (var token in jObject[node])
                yield return CreateEntity(token);
        }

        protected override IEnumerable<INode> GetChilds()
        {
            yield return BlockedUsers;
            yield return CloseFriends;
            yield return FollowRequestsSent;
            yield return Followers;
            yield return Following;
            yield return Hashtags;
        }
    }

    public class InstagramConnectionCollection : BaseInstagramCollectionElements<IInstagramConnection>, IInstagramConnectionCollection, INodeControl
    {
        public override string Name { get; }

        protected override IList<IInstagramConnection> Collection { get; }

        public InstagramConnectionCollection(string name)
        {
            Name = name;
            Collection = new List<IInstagramConnection>();
        }

        internal override void Load(JArray array)
        {

        }

        public Control CreateControl()
        {
            return new InstagramConnectionsControl(this);
        }
    }

    [DebuggerDisplay("Key : {Key}, Date : {Date}")]
    public class InstagramConnection : IInstagramConnection
    {
        public string Key { get; }

        public DateTime Date { get; }

        public InstagramConnection(string key, DateTime date)
        {
            Key = key;
            Date = date;
        }
    }
}
