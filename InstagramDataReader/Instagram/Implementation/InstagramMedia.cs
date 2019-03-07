using System;
using System.Drawing;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;
using System.Diagnostics;

namespace InstagramDataReader.Instagram
{
    public class InstagramMedias : InstagramDataElement<IInstagramMedia>, IInstagramMedias
    {
        public override string Name => "Medias";

        public virtual string File => "media.json";

        public virtual IInstagramMediaCollection<IInstagramMedia> Photos { get; protected set; }

        public virtual IInstagramMediaCollection<IInstagramProfileMedia> Profile { get; protected set; }

        public virtual IInstagramMediaCollection<IInstagramMedia> Stories { get; protected set; }

        public virtual IInstagramMediaCollection<IInstagramMedia> Direct { get; protected set; }

        protected override IInstagramMedia CreateEntity(JToken jToken)
        {
            return jToken.ToObject<InstagramMedia>();
        }

        internal override void Load(JObject jObject)
        {
            Photos = CreateCollection(jObject, new InstagramMediaCollection("Photos"), "photos");
            Profile = CreateCollection(jObject, new InstagramMediaCollection<IInstagramProfileMedia>("Profile"), "profile", CreateProfileEntity);
            Stories = CreateCollection(jObject, new InstagramMediaCollection("Stories"), "stories");
            Direct = CreateCollection(jObject, new InstagramMediaCollection("Direct"), "direct");
        }

        protected override IEnumerable<INode> GetChilds()
        {
            yield return Photos;
            yield return Profile;
            yield return Stories;
            yield return Direct;
        }

        public virtual void Dispose()
        {
            Dispose(Photos);
            Dispose(Profile);
            Dispose(Stories);
            Dispose(Direct);
        }

        private IInstagramProfileMedia CreateProfileEntity(JToken jToken)
        {
            return jToken.ToObject<InstagramProfileMedia>();
        }

        private void Dispose(IEnumerable<IDisposable> elements)
        {
            if (elements != null)
                foreach (var element in elements)
                    element?.Dispose();
        }
    }

    public class InstagramMediaCollection : InstagramMediaCollection<IInstagramMedia>
    {
        public InstagramMediaCollection(string name) 
            : base(name)
        {
        }
    }

    public class InstagramMediaCollection<TMedia> : BaseInstagramCollectionElements<TMedia>, IInstagramMediaCollection<TMedia>
    {
        public override string Name { get; }

        protected override IList<TMedia> Collection { get; }

        private InstagramMediaCollection()
        {
            Collection = new List<TMedia>();
        }

        public InstagramMediaCollection(string name)
            : this()
        {
            Name = name;
        }

        internal override void Load(JArray array)
        {
            foreach (var token in array)
                Add(token.ToObject<TMedia>());
        }
    }

    [DebuggerDisplay("TakenAt: {TakenAt}, Path: {Path}, Caption: {Caption}")]
    public class InstagramMedia : IInstagramMedia
    {
        private Image _image;

        public virtual string Caption { get; set; }
        
        [JsonProperty("taken_at")]
        public virtual DateTime TakenAt { get; set; }

        public virtual string Path { get; set; }

        public virtual Image Image
        {
            get
            {
                if (_image == null)
                    _image = Image.FromFile(Path);

                return _image;
            }
        }

        public virtual void Dispose()
        {
            _image?.Dispose();
        }
    }

    [DebuggerDisplay("TakenAt: {TakenAt}, Path: {Path}, IsActiveProfile: {IsActiveProfile}, Caption: {Caption}")]
    public class InstagramProfileMedia : InstagramMedia, IInstagramProfileMedia
    {
        [JsonProperty("is_active_profile")]
        public virtual bool IsActiveProfile { get; set; }
    }
}
