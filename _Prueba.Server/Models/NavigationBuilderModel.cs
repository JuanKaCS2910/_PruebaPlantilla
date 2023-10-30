using _Prueba.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _Prueba.Server.Models
{
    public static class NavigationBuilderModel
    {
        private const string Underscore = "_";
        private const string Dash = "-";
        private const string Space = " ";
        private static readonly string Empty = string.Empty;
        public static readonly string Void = "javascript:void(0);";

        public static SmartNavigationDTO Seed => BuildNavigation();
        public static SmartNavigationDTO Full => BuildNavigation(seedOnly: false);

        public static SmartNavigationDTO BuildNavigation(bool seedOnly = true)
        {
            var jsonText = File.ReadAllText("nav.json");
            var navigation = NavigationBuilder.FromJson(jsonText);
            var menu = FillProperties(navigation.Lists, seedOnly);

            return new SmartNavigationDTO(menu);
        }

        public static List<ListItemDTO> FillProperties(IEnumerable<ListItemDTO> items, bool seedOnly, ListItemDTO parent = null)
        {
            var result = new List<ListItemDTO>();

            foreach (var item in items)
            {
                item.Text ??= item.Title;
                item.Tags = string.Concat(parent?.Tags, Space, item.Title.ToLower()).Trim();

                var parentRoute = (Path.GetFileNameWithoutExtension(parent?.Text ?? Empty)?.Replace(Space, Underscore) ?? Empty).ToLower();
                var sanitizedHref = parent == null ? item.Href?.Replace(Dash, Empty) : item.Href?.Replace(parentRoute, parentRoute.Replace(Underscore, Empty)).Replace(Dash, Empty);
                var route = Path.GetFileNameWithoutExtension(sanitizedHref ?? Empty)?.Split(Underscore) ?? Array.Empty<string>();

                item.Route = route.Length > 1 ? $"/{route.First()}/{string.Join(Empty, route.Skip(1))}" : item.Href;

                item.I18n = parent == null
                    ? $"nav.{item.Title.ToLower().Replace(Space, Underscore)}"
                    : $"{parent.I18n}_{item.Title.ToLower().Replace(Space, Underscore)}";
                item.Type = parent == null ? item.Href == null ? ItemType.Category : ItemType.Single : item.Items.Any() ? ItemType.Parent : ItemType.Child;
                item.Items = FillProperties(item.Items, seedOnly, item);

                //if (item.Href.IsVoid() && item.Items.Any())
                //    item.Type = ItemType.Sibling;

                if (!seedOnly || item.ShowOnSeed)
                    result.Add(item);
            }

            return result;
        }

    }
}
