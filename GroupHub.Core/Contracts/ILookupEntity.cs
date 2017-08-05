using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GroupHub.Core
{
    public interface ILookupEntity : IIdentifiableEntity
    {
        
        string EnglishName { get; set; }
        string ArabicName { get; set; }
        
        string LocalizedName
        {
            get;
        }
    }
}
