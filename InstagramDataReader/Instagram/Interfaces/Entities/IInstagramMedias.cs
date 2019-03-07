using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramMedias : IRootNode, IDisposable
    {
        IInstagramMediaCollection<IInstagramMedia> Photos { get; }
        IInstagramMediaCollection<IInstagramProfileMedia> Profile { get; }
        IInstagramMediaCollection<IInstagramMedia> Stories { get; }
        IInstagramMediaCollection<IInstagramMedia> Direct { get; }
    }

    public interface IInstagramMediaCollection<TMedia> : INode, IEnumerable<TMedia>
    {

    }

    public interface IInstagramMedia : IPhoto
    {
        string Caption { get; }
        DateTime TakenAt { get; }
        string Path { get; }
    } 

    public interface IInstagramProfileMedia : IInstagramMedia
    {
        bool IsActiveProfile { get; }
    }
}