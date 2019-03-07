using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramSearches : IRootNode, IEnumerable<IInstagramSearch>
    {
    }

    public interface IInstagramSearch
    {
        string Search { get; }
        DateTime Date { get; }
        string Type { get; }
    }
}