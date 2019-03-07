using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using InstagramDataReader.Data;
using InstagramDataReader.Interfaces;
using InstagramDataReader.Instagram.Controls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramDataReader.Instagram
{
    public class InstagramConversations : BaseInstagramCollectionElements<IInstagramConversation>, IInstagramConversations
    {
        private IList<IInstagramConversation> _collection;

        public override string Name => "Messages";

        public virtual string File => "messages.json";

        protected override IList<IInstagramConversation> Collection => _collection;

        public InstagramConversations()
        {
            _collection = new List<IInstagramConversation>();
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<InstagramConversation>());

            _collection = Collection.GroupBy(c => c.Name)
                                    .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                                    .Select(GroupConversations)
                                    .ToList();
        }

        protected override IEnumerable<INode> GetChilds()
        {
            return Collection;
        }

        private IInstagramConversation GroupConversations(IEnumerable<IInstagramConversation> conversations)
        {
            var convs = conversations.ToList();

            if (convs.Count <= 0)
                return new InstagramConversation();

            return new InstagramConversation
            {
                Participants = convs[0].Participants,
                Messages = convs.SelectMany(m => m.Messages)
                                .OrderBy(m => m.Date)
            };
        }
    }

    [DebuggerDisplay("Name : {Name}")]
    public class InstagramConversation : IInstagramConversation, INodeControl
    {
        public virtual Guid Id { get; } = Guid.NewGuid();

        public virtual string Name => string.Join(", ", Participants ?? Enumerable.Empty<string>());

        public virtual IEnumerable<string> Participants { get; set; }

        [JsonProperty("conversation")]
        [JsonConverter(typeof(ConcreteTypeColectionConverter<IInstagramMessage, InstagramMessage>))]
        public virtual IEnumerable<IInstagramMessage> Messages { get; set; }
        
        public IEnumerable<INode> Childs => Enumerable.Empty<INode>();

        public void RemoveParticipant(string participant)
        {
            Participants = Participants.Where(p => p != participant);
        }

        public Control CreateControl()
        {
            return new InstagramConversationsControl(this);
        }
    }

    [DebuggerDisplay("Sender : {Sender}, At: {Date}, Text : {Text}")]
    public class InstagramMessage : IInstagramMessage
    {
        public string Sender { get; set; }

        [JsonProperty("created_at")]
        public DateTime Date { get; set; }

        [JsonProperty("story_share")]
        public string StoryShare { get; set; }

        public string Text { get; set; }

        [JsonProperty("animated_media_images")]
        [JsonConverter(typeof(ConcreteTypeConverter<InstagramMessageMedia>))]
        public IInstagramMessageMedia Media { get; set; }
    }

    public class InstagramMessageMedia : IInstagramMessageMedia
    {
        [JsonConverter(typeof(ConcreteTypeConverter<InstagramMessageMediaOriginal>))]
        public IInstagramMessageMediaOriginal Original { get; set; }
    }

    [DebuggerDisplay("Url : {Url}")]
    public class InstagramMessageMediaOriginal : IInstagramMessageMediaOriginal
    {
        public string Url { get; set; }
    }
}
