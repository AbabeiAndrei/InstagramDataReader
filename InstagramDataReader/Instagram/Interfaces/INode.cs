using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface INode : IIdentificable
    {
        string Name { get; }
        IEnumerable<INode> Childs { get; }
    }
}