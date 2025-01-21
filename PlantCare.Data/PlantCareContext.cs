using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantCare.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantCare.Data
{
    public class PlantCareContext : IdentityDbContext
    {
        public DbSet<Plant> Plants { get; set; }

        public DbSet<HomeTip> HomeTips { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public PlantCareContext(DbContextOptions<PlantCareContext> ctx)
            : base(ctx)
        {

        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>()
                .HasMany(m => m.HomeTips)
                .WithOne(r => r.Plant)
                .HasForeignKey(r => r.PlantId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
