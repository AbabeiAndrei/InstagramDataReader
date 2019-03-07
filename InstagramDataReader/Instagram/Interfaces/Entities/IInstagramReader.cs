using System;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramReader : INode, IDisposable
    {
        string Directory { get; }
        IInstagramComments Comments { get; }
        IInstagramConnections Connections { get; }
        IInstagramContacts Contacts { get; }
        IInstagramLikes Likes { get; }
        IInstagramMedias Medias { get; }
        IInstagramConversations Conversations { get; }
        IInstagramProfile Profile { get; }
        IInstagramSaved Saved { get; }
        IInstagramSearches Searches { get; }
        IInstagramSettings Settings { get; }
    }
}