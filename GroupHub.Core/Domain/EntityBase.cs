using GroupHub.Core.Domain;
using System;

namespace GroupHub.Core.Domain
{
    public abstract class EntityBase : IEntity
    {

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? UpdateDate { get; set; }
        
        public string UpdatedBy { get; set; }

    }
}