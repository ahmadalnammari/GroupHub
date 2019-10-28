using GroupHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupHub.Core.Services
{
    public interface ISvcUser : ISvc
    {

        Task<User> Add(User user);
        User Get(int id);
    }
}
