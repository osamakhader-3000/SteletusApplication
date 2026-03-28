using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SteletusApplicationDataAccess.Models
{
    public partial class SteletusApplicationDataAccessContext : DbContext
    {
        public virtual DbSet<SteletusApplicationDataAccess.Models.Task> Tasks { get; set; }

        public virtual DbSet<SteletusApplicationDataAccess.Models.Build> Builds { get; set; }

        public virtual DbSet<SteletusApplicationDataAccess.Models.BuildClient> BuildClients { get; set; }


        public SteletusApplicationDataAccessContext(DbContextOptions<SteletusApplicationDataAccessContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
           
            OnModelCreatingPartial(modelBuilder);


            modelBuilder.Entity<BuildClient>()
               .HasOne(bc => bc.Build)
               .WithMany(b => b.Clients)
               .HasForeignKey(bc => bc.BuildId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }

}
