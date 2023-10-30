using _Prueba.Shared;
using System.Diagnostics;
using System.Security.Claims;

namespace _Prueba.Web.Extensions
{
    public static class IdentityExtensions
    {
        [DebuggerStepThrough]
        private static bool HasRole(this ClaimsPrincipal principal, params string[] roles)
        {
            if (principal == null)
                return default;

            var claims = principal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToSafeList();

            return claims?.Any() == true && claims.Intersect(roles ?? new string[] { }).Any();
        }

        [DebuggerStepThrough]
        public static IEnumerable<ListItemDTO> AuthorizeFor(this IEnumerable<ListItemDTO> source, ClaimsPrincipal identity)
            => source.Where(x => x.Roles.IsNullOrEmpty() || (x.Roles.HasItems() && identity.HasRole(x.Roles))).ToSafeList();

        //[DebuggerStepThrough]
        //public static HtmlString AsRaw(this string value) => new HtmlString(value);

        [DebuggerStepThrough]
        public static string ToPage(this string href) => System.IO.Path.GetFileNameWithoutExtension(href)?.ToLower();

        [DebuggerStepThrough]
        public static bool IsVoid(this string href) => href?.ToLower() == "javascript:void(0);";//NavigationBuilderModel.Void;

        [DebuggerStepThrough]
        public static bool IsRelatedTo(this ListItemDTO item, string pageName) => item?.Type == ItemType.Parent && item?.Href?.ToPage() == pageName?.ToLower();

    }
}
