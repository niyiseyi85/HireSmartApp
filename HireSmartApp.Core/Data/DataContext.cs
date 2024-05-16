using HireSmartApp.Core.Models.Domain.Authorization;
using HireSmartApp.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace HireSmartApp.Core.Data
{
    public class DataContext : DbContext
    {        
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {          ;
            _config = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AuthClaim> AuthClaims { get; set; }
        public virtual DbSet<RoleAuthClaim> RoleAuthClaims { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<ApplicationProgram> ApplicationPrograms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var url = _config["AzureCosmos:EndpointUri"];
            var accountKey = _config["AzureCosmos:PrimaryKey"];
            var databaseName = _config["AzureCosmos:Database"];

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseCosmos(url, accountKey, databaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
  .ToContainer(nameof(User))
                .HasPartitionKey(c => c.Id)
                .HasNoDiscriminator();

            modelBuilder.Entity<Questions>()
             .ToContainer(nameof(Questions))
             .HasPartitionKey(o => o.Id)
             .HasNoDiscriminator();

            modelBuilder.Entity<ApplicationProgram>()
             .ToContainer(nameof(ApplicationProgram))
             .HasPartitionKey(c => c.Id)
             .HasNoDiscriminator();
             
            modelBuilder.Entity<AuthClaim>()
            .HasKey(ac => ac.ClaimId);

            modelBuilder.Entity<RoleAuthClaim>()
            .HasKey(rc => new { rc.RoleId, rc.AuthClaimId });

            modelBuilder.Entity<RoleAuthClaim>()
            .HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaims)
            .HasForeignKey(rc => rc.RoleId);

            modelBuilder.Entity<RoleAuthClaim>()
                .HasOne(rc => rc.AuthClaim)
                .WithMany(a => a.RoleAuthClaims)
                .HasForeignKey(rc => rc.AuthClaimId);
        }
    }
}
