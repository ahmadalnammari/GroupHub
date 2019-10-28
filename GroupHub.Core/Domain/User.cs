using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Domain
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Name123 { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }


    }
}
