using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Domain
{
    public class Group : EntityBase
    {
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public int UserId { get; set; }
    }
}
