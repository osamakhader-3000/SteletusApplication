using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SteletusApplicationDataAccess.Models
{
    public partial class SteletusApplicationDataAccessContext : DbContext
    {
        public virtual DbSet<SteletusApplicationDataAccess.Models.Task> Tasks { get; set; }


        public SteletusApplicationDataAccessContext(DbContextOptions<SteletusApplicationDataAccessContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }

}
