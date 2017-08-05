using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GroupHub.Apis
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                this.HandleUnauthorizedRequest(actionContext);
            }

            #region Validate access token stamp


            //var accessTokenStamp = claimsIdentity.Claims.FirstOrDefault(c => c.Type == AmwalClaimsTypes.AccessTokenStamp);

            //if (accessTokenStamp == null)
            //{
            //    // There was no stamp in the token.
            //    this.HandleUnauthorizedRequest(actionContext);
            //}
            //else
            //{
            //    var userId = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            //    if (!ValidateAccessTokenStamp(int.Parse(userId), accessTokenStamp.Value))
            //    {
            //        throw new UnauthorizedAccessException();
            //    }
            //}

            #endregion


            base.OnAuthorization(actionContext);
        }


        //public bool ValidateAccessTokenStamp(int userId, string accessTokenStamp)
        //{
        //    string accessTokenStampClaimCacheKey = AmwalClaimsTypes.AccessTokenStamp + "_" + userId;
        //    //string savedAccessTokenStamp = GlobalCachingProvider.Instance.GetItem<string>(accessTokenStampClaimCacheKey, false);
        //    string savedAccessTokenStamp = null;

        //    if (savedAccessTokenStamp == null)
        //    {
        //        //load it from db because may be removed from cache
        //        UserService userService = new UserService();
        //        User user = userService.GetUserByID(userId);

        //        if (user != null && user.AccessTokenStamp == accessTokenStamp)
        //        {
        //            //GlobalCachingProvider.Instance.RemoveItem(accessTokenStampClaimCacheKey);
        //            //GlobalCachingProvider.Instance.AddItem(accessTokenStampClaimCacheKey, user.AccessTokenStamp, 20);

        //            return true;
        //        }

        //        return false;
        //    }

        //    if (savedAccessTokenStamp == accessTokenStamp)
        //        return true;
        //    return false;

        //}


    }
}