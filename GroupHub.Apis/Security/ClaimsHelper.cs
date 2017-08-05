using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using GroupHub.Core;

namespace GroupHub.Apis
{
    public class ClaimsHelper
    {

        public static List<Claim> GenerateClaims(IIdentity user)
        {
            if (user == null)
                return null;

            List<Claim> claims = new List<Claim>();

            if (user.Email != null)
                claims.Add(new Claim(ClaimTypes.Name, user.Email));

            if (user.Id != null)
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            
            return claims;
        }




    }
}