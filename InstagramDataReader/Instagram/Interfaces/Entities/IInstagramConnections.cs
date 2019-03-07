using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramConnections : IRootNode
    {
        IInstagramConnectionCollection BlockedUsers { get; }
        IInstagramConnectionCollection CloseFriends { get; }
        IInstagramConnectionCollection FollowRequestsSent { get; }
        IInstagramConnectionCollection Followers { get; }
        IInstagramConnectionCollection Following { get; }
        IInstagramConnectionCollection Hashtags { get; }
    }

    public interface IInstagramConnectionCollection : INode, IEnumerable<IInstagramConnection>
    {

    }

    public interface IInstagramConnection
    {
        string Key { get; }
        DateTime Date { get; }
    }
}