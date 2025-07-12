namespace RVASispit.Models
{
    public class Faktura
    {
        public int Id { get; set; }

        // Strani kljuc za paketKorisnika
        public int paketKorisnikaID { get; set; }
        public PaketKorisnika paketKorisnika { get; set; }  
        

        public decimal cena{ get; set; } = 0.0m;
        public DateTime datumIzdavanja { get; set; } = DateTime.UtcNow;
        public string? tekstFakture { get; set; } = string.Empty;
        public bool placeno { get; set; } = false;
        public DateTime? datumPlacanja { get; set; } = null;


    }
}