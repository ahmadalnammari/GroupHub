using GroupHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Services
{
    public interface ISvcUser : ISvc
    {

        User Add(User user);
        User Get(int id);
    }
}
