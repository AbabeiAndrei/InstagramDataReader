using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramLikes : IRootNode
    {
        IInstagramLikeCollection MediaLikes { get; }
        IInstagramLikeCollection CommentLikes { get; }
    }

    public interface IInstagramLikeCollection : INode, IEnumerable<IInstagramLike>
    {

    }

    public interface IInstagramLike
    {
        DateTime Date { get; }
        string Like { get; }
    }
}