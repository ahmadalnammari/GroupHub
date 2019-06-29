using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Domain
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }
    }
}
