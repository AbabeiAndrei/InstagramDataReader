using Newtonsoft.Json.Linq;

namespace InstagramDataReader.Interfaces
{
    internal interface IInstagramCollectionElements
    {
        void Load(JArray array);
    }
}