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

        public virtual DbSet<SteletusApplicationDataAccess.Models.Client> Clients { get; set; }

        public virtual DbSet<SteletusApplicationDataAccess.Models.BuildLog> BuildLogs { get; set; }


        public SteletusApplicationDataAccessContext(DbContextOptions<SteletusApplicationDataAccessContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(e => e.Builds)
                      .WithMany(e => e.Clients)
                      .UsingEntity<Dictionary<string, object>>(
                          "BuildClients",
                          j => j
                              .HasOne<Build>()
                              .WithMany()
                              .HasForeignKey("BuildId")
                              .HasPrincipalKey(nameof(Build.Id)),
                          j => j
                              .HasOne<Client>()
                              .WithMany()
                              .HasForeignKey("ClientId")
                              .HasPrincipalKey(nameof(Client.Id)),
                          j =>
                          {
                              j.ToTable("BuildClients");
                              j.HasKey("BuildId", "ClientId");
                          });
            });

            modelBuilder.Entity<Build>(entity =>
            {
                entity.HasMany(e => e.BuildLogs)
                      .WithOne(e=>e.Build)
                      .HasForeignKey(e => e.BuildId);
            });


            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }

}
