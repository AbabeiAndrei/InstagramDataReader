using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramComments : IRootNode
    {
        IInstagramCommentCollection MediaComments { get; }
        IInstagramCommentCollection LiveComments { get; }
    }

    public interface IInstagramCommentCollection : INode, IEnumerable<IInstagramComment>
    {

    }

    public interface IInstagramComment
    {
        DateTime Date { get; }
        string User { get; }
        string Comment { get; }
    }
}