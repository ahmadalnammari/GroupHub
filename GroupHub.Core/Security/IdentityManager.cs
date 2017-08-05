
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace GroupHub.Core
{
    public static class IdentityManager
    {
        public const string ShopIdClaimKey = "ShopId";

        public static int GetCurrentLanguage()
        {
            try
            {
                IEnumerable<string> headerValues = HttpContext.Current.Request.Headers.GetValues("languageId");
                return Convert.ToInt32(headerValues.FirstOrDefault());
            }
            catch
            { return 1; }
        }

        public static int GetLoggedInUserId()
        {
            // return int.Parse(HttpContext.Current.User.Identity.GetUserId());

            return 1;
        }

        public static CurrentUser GetCurrentUser()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            CurrentUser currentUser = new CurrentUser();

            currentUser.Id = int.Parse(HttpContext.Current.User.Identity.GetUserId());
            currentUser.Email = HttpContext.Current.User.Identity.GetUserName();

            return currentUser;
        }


        public static int GetCurrentShopId()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            int shopId = Convert.ToInt32(identity.FindFirst(ShopIdClaimKey).Value);

            return shopId;
        }


        public static int GetCurrentCountryId()
        {
            return 1;
        }

    }
}