using System.Security.Claims;

namespace Tor
{
    public static class clsCommon
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;

            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
