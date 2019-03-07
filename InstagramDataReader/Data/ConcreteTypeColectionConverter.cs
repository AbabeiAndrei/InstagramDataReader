using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace InstagramDataReader.Data
{
    public class ConcreteTypeColectionConverter<TInterface, TItem> : JsonConverter where TItem : TInterface
    {
        public override bool CanConvert(Type objectType)
        {
            //this method is *not* called when using the JsonConverterAttribute to assign a converter
            return IsIList(objectType) || objectType.GetInterfaces().Any(IsIList);
        }

        private static bool IsIList(Type objectType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            //explicitly specify the concrete type we want to create
            var specializedList = serializer.Deserialize<IEnumerable<TItem>>(reader);
            return specializedList.Cast<TInterface>().ToList();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine
            serializer.Serialize(writer, value);
        }
    }
}
