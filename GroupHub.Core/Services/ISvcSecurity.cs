using GroupHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Services
{
    public interface ISvcSecurity : ISvc
    {
        User Authenticate(string email, string password);
    }
}
