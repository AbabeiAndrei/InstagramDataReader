using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;
using InstagramDataReader.Instagram.Controls;

namespace InstagramDataReader.Instagram
{
    public class InstagramContacts : BaseInstagramCollectionElements<IInstagramContact>, IInstagramContacts, INodeControl
    {
        public override string Name => "Contacts";

        public virtual string File => "contacts.json";

        protected override IList<IInstagramContact> Collection { get; }

        public InstagramContacts()
        {
            Collection = new List<IInstagramContact>();
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<InstagramContact>());
        }

        public Control CreateControl()
        {
            return new InstagramConversationControl(this);
        }
    }

    [DebuggerDisplay("FirstName: {FirstName}, LastName: {LastName}, Contact: {Contact}")]
    public class InstagramContact : IInstagramContact
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public string Contact { get; set; }
    }
}
