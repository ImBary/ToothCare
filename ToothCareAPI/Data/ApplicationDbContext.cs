using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToothCareAPI.Model;

namespace ToothCareAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Clients> Client { get; set; }
        public DbSet<Doctors> Doctor { get; set; }
        public DbSet<Appointments> Appointment { get; set; }
        public DbSet<Procedures> Procedure { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(a => a.Doctors)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Procedures)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.ProcedureId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Clients)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(a => a.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Pesel).IsUnique();

                entity.HasMany(c => c.Appointments)
                    .WithOne(a => a.Clients)
                    .HasForeignKey(a => a.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Doctors>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(d => d.Appointments)
                    .WithOne(a => a.Doctors)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
