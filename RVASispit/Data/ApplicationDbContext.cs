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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API konfiguracija za relacije

            modelBuilder.Entity<PaketKorisnika>()
                .HasOne(pk => pk.korisnik)
                .WithMany(u => u.PaketiKorisnikas)
                .HasForeignKey(pk => pk.korisnikID);

            modelBuilder.Entity<PaketKorisnika>()
                .HasOne(pk => pk.tipPaketa)
                .WithMany(tp => tp.TipPaketaPaketKorisnika)
                .HasForeignKey(pk => pk.tipPaketaID);

            modelBuilder.Entity<Faktura>()
                .HasOne(f => f.paketKorisnika)
                .WithMany(pk => pk.fakture)
                .HasForeignKey(f => f.paketKorisnikaID);
            
            
            modelBuilder.Entity<Faktura>()
               .Property(f => f.cena)
               .HasPrecision(18, 2);

            modelBuilder.Entity<Faktura>()
                .Property(f => f.datumIzdavanja)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaGodisnjePretplate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaMesecnePretplate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TipPaketa>()
                .Property(t => t.cenaRezervacije)
                .HasPrecision(18, 2);


            modelBuilder.Entity<TipPaketa>()
                .HasMany(tp => tp.TipPaketaPaketKorisnika)
                .WithOne(pk => pk.tipPaketa)
                .HasForeignKey(pk => pk.tipPaketaID);
        }

    }
}
