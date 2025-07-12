namespace RVASispit.Models
{
    public class TipPaketa
    {
        public int Id { get; set; }
        public decimal cenaGodisnjePretplate { get; set; }
        public decimal cenaMesecnePretplate { get; set; }
        public string opisPaketa { get; set; } = string.Empty;
        public string nazivPaketa { get; set; } = string.Empty;
        public decimal cenaRezervacije { get; set; } 

        //public List<TipPaketaBiljke> TipPaketaBiljke{ get; set; } = new();
        public List<PaketKorisnika> TipPaketaPaketKorisnika { get; set; } = new();


    }
}
