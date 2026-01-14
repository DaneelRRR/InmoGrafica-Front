using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Foto>()
                .HasOne(f => f.Inmueble)
                .WithMany(i => i.Fotos)
                .HasForeignKey(f => f.InmuebleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
