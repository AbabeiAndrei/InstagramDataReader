using System;
using System.IO;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;
using System.Collections.Generic;

namespace InstagramDataReader.Instagram
{
    public class InstagramReader : IInstagramReader
    {
        private string _directory;
        private bool _loaded;
        private readonly IInstagramComments _comments;
        private readonly IInstagramConnections _connections;
        private readonly IInstagramContacts _contacts;
        private readonly InstagramLikes _likes;
        private readonly IInstagramMedias _medias;
        private readonly IInstagramConversations _conversations;
        private readonly IInstagramProfile _profile;
        private readonly IInstagramSaved _saved;
        private readonly IInstagramSearches _searches;
        private readonly IInstagramSettings _settings;

        public IEnumerable<INode> Childs => GetRootNodes();

        public virtual bool Loaded => _loaded;
        
        public virtual string Directory
        {
            get => _directory;
            protected set => _directory = value;
        }

        public virtual Guid Id { get; } = Guid.NewGuid();

        public virtual string Name => "Instagram Data";

        public virtual IInstagramComments Comments
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _comments;
            }
        }

        public virtual IInstagramConnections Connections
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _connections;
            }
        }

        public virtual IInstagramContacts Contacts
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _contacts;
            }
        }

        public virtual IInstagramLikes Likes
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _likes;
            }
        }

        public virtual IInstagramMedias Medias
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _medias;
            }
        }

        public virtual IInstagramConversations Conversations
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _conversations;
            }
        }

        public virtual IInstagramProfile Profile
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _profile;
            }
        }

        public virtual IInstagramSaved Saved
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _saved;
            }
        }

        public virtual IInstagramSearches Searches
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _searches;
            }
        }

        public virtual IInstagramSettings Settings
        {
            get
            {
                if (!Loaded)
                    throw new InstagramReaderNotLoadedException();

                return _settings;
            }
        }

        protected InstagramReader(string directory)
        {
            _directory = directory;

            _comments = new InstagramComments();
            _connections = new InstagramConnections();
            _contacts = new InstagramContacts();
            _likes = new InstagramLikes();
            _medias = new InstagramMedias();
            _conversations = new InstagramConversations();
            _profile = new InstagramProfile();
            _saved = new InstagramSaved();
            _searches = new InstagramSearches();
            _settings = new InstagramSettings();
        }

        private IEnumerable<IRootNode> GetRootNodes()
        {
            yield return _comments;
            yield return _connections;
            yield return _contacts;
            yield return _likes;
            yield return _medias;
            yield return _conversations;
            yield return _profile;
            yield return _saved;
            yield return _searches;
            yield return _settings;
        }

        public static async Task<InstagramReader> CreateReader(string directory)
        {
            var reader = new InstagramReader(directory);

            await reader.Load();

            return reader;
        }

        protected virtual async Task Load()
        {
            if (string.IsNullOrEmpty(Directory))
                throw new ArgumentNullException(nameof(Directory));

            foreach (var node in GetRootNodes())
                await CreateElement(node);

            _loaded = true;
        }

        protected virtual async Task<T> CreateElement<T>(T element)
            where T : IRootNode
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var filePath = Path.Combine(Directory, element.File);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File for element not found", filePath);

            using (var fileReader = File.OpenText(filePath))
            using (var reader = new JsonTextReader(fileReader))
                if (element is IInstagramCollectionElements collection)
                    collection.Load(await JArray.LoadAsync(reader));
                else
                    element.Load(await JObject.LoadAsync(reader));

            return element;
        }

        public virtual void Dispose()
        {
            _medias?.Dispose();
        }
    }
}
