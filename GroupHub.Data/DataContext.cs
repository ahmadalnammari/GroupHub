using GroupHub.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GroupHub.Data
{
    public class GroupHubContext : DbContext
    {

        public GroupHubContext() : base("name=GroupHub")

        {


            Database.SetInitializer<GroupHubContext>(null);


            base.Configuration.LazyLoadingEnabled = false;
            base.Configuration.ProxyCreationEnabled = false;

        }

        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IIdentifiableEntity>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    // modify updated date and updated by column for 
                    // adds of updates.
                    auditableEntity.Entity.ModifiedDate = DateTime.Now;
                    //auditableEntity.Entity.UpdatedBy = currentUser;

                    // pupulate created date and created by columns for
                    // newly added record.
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        // we also want to make sure that code is not inadvertly
                        // modifying created date and created by columns 
                        auditableEntity.Property(p => p.CreatedDate).IsModified = false;
                        //auditableEntity.Property(p => p.CreatedBy).IsModified = false;
                    }
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


        }

    }
}
