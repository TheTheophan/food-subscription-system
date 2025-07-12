namespace RVASispit.Models
{
    public class PaketKorisnika
    {
        public int Id { get; set; }
        public bool godisnjaPretplata { get; set; }

        // Strani kljuc za tip paketa
        public int tipPaketaID { get; set; }
        public required TipPaketa tipPaketa { get; set; }

        // Strani kljuc za korisnika
        public string korisnikID { get; set; }
        public required ApplicationUser korisnik { get; set; }

        public List<Faktura> fakture { get; set; } = new();

    }
}
