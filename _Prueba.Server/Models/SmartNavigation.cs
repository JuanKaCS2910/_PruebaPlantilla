using _Prueba.Shared;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace _Prueba.Server.Models
{
    internal static class NavigationBuilder
    {
        private static JsonSerializerOptions DefaultSettings => SerializerSettings();

        private static JsonSerializerOptions SerializerSettings(bool indented = true)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = indented,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return options;
        }

        public static SmartNavigation FromJson(string json) => JsonSerializer.Deserialize<SmartNavigation>(json, DefaultSettings);
    }

    public class SmartNavigation
    {
        public SmartNavigation()
        {
        }

        public SmartNavigation(IEnumerable<ListItemDTO> items)
        {
            Lists = new List<ListItemDTO>(items);
        }

        public string Version { get; set; }
        public List<ListItemDTO> Lists { get; set; } = new List<ListItemDTO>();

    }
}
