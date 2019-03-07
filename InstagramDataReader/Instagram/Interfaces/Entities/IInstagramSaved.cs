using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramSaved : IRootNode
    {
        IInstagramSavedCollections Collections { get; }
        IEnumerable<IInstagramSavedMedia> Medias { get; }
    }

    public interface IInstagramSavedCollections : INode, IEnumerable<IInstagramSavedCollection>
    {
    }

    public interface IInstagramSavedCollection : INode
    {
        DateTime Created { get; }
        DateTime Updated { get; }
        IEnumerable<IInstagramSavedMedia> Media { get; }
    }

    public interface IInstagramSavedMedia
    {
        DateTime Key { get; }
        string Value { get; }
    }
}