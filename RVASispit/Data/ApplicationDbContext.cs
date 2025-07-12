using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RVASispit.Models;

namespace RVASispit.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PaketKorisnika> PaketiKorisnikas { get; set; }
        public DbSet<TipPaketa> TipPaketas { get; set; }
        public DbSet<Faktura> Fakturas { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configuration for PaketKorisnika - korisnik
            modelBuilder.Entity<PaketKorisnika>()
                .HasOne(pk => pk.korisnik)
                .WithMany(u => u.PaketiKorisnikas)
                .HasForeignKey(pk => pk.korisnikID);

            // Fluent API configuration for PaketKorisnika - tipPaketa
            modelBuilder.Entity<PaketKorisnika>()
                .HasOne(pk => pk.tipPaketa)
                .WithMany(tp => tp.TipPaketaPaketKorisnika)
                .HasForeignKey(pk => pk.tipPaketaID);

            // Fluent API configuration for Faktura - paketKorisnika
            modelBuilder.Entity<Faktura>()
                .HasOne(f => f.paketKorisnika)
                .WithMany(pk => pk.fakture)
                .HasForeignKey(f => f.paketKorisnikaID);
            
            
            // Set decimal precision for TipPaketa and Faktura properties
            modelBuilder.Entity<Faktura>()
               .Property(f => f.cena)
               .HasPrecision(18, 2);
            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaGodisnjePretplate)
                .HasPrecision(18, 2);
            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaMesecnePretplate)
                .HasPrecision(18, 2);
            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaRezervacije)
                .HasPrecision(18, 2);


            // Fluent API configuration for TipPaketa - PaketKorisnika
            modelBuilder.Entity<TipPaketa>()
                .HasMany(tp => tp.TipPaketaPaketKorisnika)
                .WithOne(pk => pk.tipPaketa)
                .HasForeignKey(pk => pk.tipPaketaID);
        }

    }
}
