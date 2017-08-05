using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GroupHub.Core
{
    public interface IIdentifiableEntity
    {

        int Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime ModifiedDate { get; set; }
    }
}
