using GroupHub.Core.Domain;
using GroupHub.Core.Services;
using GroupHub.Infra;
using GroupHub.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupHub.Infra.Services
{
    public class SvcSecurity : SvcBase<User>, ISvcSecurity
    {
        public SvcSecurity(GroupHubContext context) : base(context)
        {
        }


        public void Register(User user)
        {

            using (var db = new GroupHubContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

        }



        public User Authenticate(string email, string hashedPassword)
        {
            User user = null;

            using (var db = new GroupHubContext())
            {
                user = db.Users.SingleOrDefault(
   e => e.Email == email && e.HashedPassword == hashedPassword);

            }

            return user;
        }


    }
}