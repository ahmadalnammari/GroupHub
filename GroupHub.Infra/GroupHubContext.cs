using GroupHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Infra
{
    public class GroupHubContext : DbContext, IMigrationContext
    {
        public GroupHubContext() 
        {

        }

        public GroupHubContext(DbContextOptions options) : base(options) { 
        
        }



        public DbSet<User> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("User ID=postgres;Password=0060501;Host=localhost;Port=5432;Database=my_db;Pooling=true;");
    }






}
