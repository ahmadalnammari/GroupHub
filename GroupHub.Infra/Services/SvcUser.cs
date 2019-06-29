using GroupHub.Core.Domain;
using GroupHub.Core.Services;
using GroupHub.Infra;
using GroupHub.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupHub.Infra.Services
{
    public class SvcUser : Svc, ISvcUser
    {

        public SvcUser() : base()
        {

        }
        

        public User Add(User user)
        {

            using (var db = new GroupHubContext())
            {
                db.Users.Add(user);
                
                db.SaveChanges();
            }

            return user;
        }


        public User Get(int id)
        {
            User user = null;

            using (var db = new GroupHubContext())
            {
                user= db.Users.FirstOrDefault(u => u.Id == id);
                
            }

            return user;
        }

    }
}