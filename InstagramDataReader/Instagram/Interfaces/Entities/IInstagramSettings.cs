using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramSettings : IRootNode
    {
        string AllowCommentsFrom { get; }
        IEnumerable<string> BlockedCommenters { get; }
        IEnumerable<string> FilteredKeywords { get; }
    }
}