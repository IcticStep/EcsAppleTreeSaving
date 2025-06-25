using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

namespace Code.Runtime.Infrastructure.Progress.Extensions
{
    public static class JsonSerializationExtensions
    {
        public static string ToJson<T>(this T self) =>
            JsonConvert.SerializeObject(
                self, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                });

        public static T FromJson<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(
                json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                });

        static JsonSerializationExtensions()
        {
            // ReSharper disable once ObjectCreationAsStatement
            AotHelper.Ensure(() => new ReferenceConverter(typeof(Dummy)));
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public class Dummy { }
    }
}