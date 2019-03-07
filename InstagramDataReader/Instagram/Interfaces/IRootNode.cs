using Newtonsoft.Json.Linq;

namespace InstagramDataReader.Interfaces
{
    public interface IRootNode : ILoadeable, INode
    {
        string File { get; }
    }

    public interface ILoadeable
    {
        void Load(JObject jObject);
    }
}