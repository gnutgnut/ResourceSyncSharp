using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace ResourceSyncSharp.Codecs
{
    [PublicAPI]
    public static class JsonCodec
    {
        [Pure]
        [PublicAPI]
        public static T FromJson<T>(string source)
        {
            var jsons = GetJsonSerializer();
            var reader = new StringReader(source);
            using (var jsonReader = new JsonTextReader(reader))
            {
                return jsons.Deserialize<T>(jsonReader);
            }
        }

        [Pure]
        [PublicAPI]
        public static string ToJson(object obj)
        {
            var jsons = GetJsonSerializer();
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                jsons.Serialize(writer, obj);
            }
            return sb.ToString();
        }

        [Pure]
        private static JsonSerializer GetJsonSerializer()
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                Culture = CultureInfo.InvariantCulture,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };
            return JsonSerializer.Create(settings);
        }
    }
}