using GroupHub.Core.Domain;
using GroupHub.Core.Services;
using GroupHub.Infra;
using GroupHub.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupHub.Infra.Services
{
    public class SvcUser : SvcBase<User>, ISvcUser
    {

        public SvcUser(GroupHubContext context) : base(context)
        {

        }
        

        public async Task<User> Add(User user)
        {

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
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