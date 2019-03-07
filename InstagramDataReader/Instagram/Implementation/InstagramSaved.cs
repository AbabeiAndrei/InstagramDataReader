using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram
{
    public class InstagramSaved : InstagramDataElement<IInstagramSavedMedia>, IInstagramSaved
    {
        public override string Name => "Savings";

        public virtual string File => "saved.json";

        public virtual IInstagramSavedCollections Collections { get; protected set; }

        public virtual IEnumerable<IInstagramSavedMedia> Medias { get; protected set; }

        protected override IInstagramSavedMedia CreateEntity(JToken jToken)
        {
            return new InstagramSavedMedia
            {
                Key = jToken[0].Value<DateTime>(),
                Value = jToken[1].Value<string>()
            };
        }

        internal override void Load(JObject jObject)
        {
            Medias = ReadEntities(jObject, "saved_media");
            var collections = CreateCollection(jObject, new InstagramSavedCollections("Collections"), "saved_collections", CreateSavedCollectionEntity);
            collections.Insert(0, CreateAllSavedCollection(Medias));
            Collections = collections;
        }

        private IInstagramSavedCollection CreateAllSavedCollection(IEnumerable<IInstagramSavedMedia> medias)
        {
            return new InstagramSavedCollection
            {
                Name = InstagramSavedCollection.All,
                Media = medias
            };
        }

        private IInstagramSavedCollection CreateSavedCollectionEntity(JToken token)
        {
            var entity = token.ToObject<InstagramSavedCollection>();

            entity.Media = token["media"].Select(CreateEntity);

            return entity;
        }

        protected override IEnumerable<INode> GetChilds()
        {
            return Collections;
        }
    }

    public class InstagramSavedCollections : BaseInstagramCollectionElements<IInstagramSavedCollection>, IInstagramSavedCollections
    {
        private readonly string _name;

        public override string Name => _name;

        protected override IList<IInstagramSavedCollection> Collection { get; }
        
        private InstagramSavedCollections()
        {
            Collection = new List<IInstagramSavedCollection>();
        }

        public InstagramSavedCollections(string name)
            : this()
        {
            _name = name;
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<InstagramSavedCollection>());
        }
    }

    [DebuggerDisplay("{Name}")]
    public class InstagramSavedCollection : IInstagramSavedCollection
    {
        public const string All = "General";

        public virtual Guid Id { get; } = Guid.NewGuid();

        public virtual string Name { get; set; }

        [JsonProperty("created_at")]
        public virtual DateTime Created { get; set; }

        [JsonProperty("updated_at")]
        public virtual DateTime Updated { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<IInstagramSavedMedia> Media { get; set; }

        public IEnumerable<INode> Childs => Enumerable.Empty<INode>();
    }

    [DebuggerDisplay("Key: {Key}, Value: {Value}")]
    public class InstagramSavedMedia : IInstagramSavedMedia
    {
        public DateTime Key { get; set; }

        public string Value { get; set; }
    }
}
