using Microsoft.AspNetCore.Mvc.Rendering;

namespace RVASispit.Models.ViewModels
{
    public class PaketKorisnikaCreateViewModel
    {
        public string NazivPaketa { get; set; } = string.Empty;
        public bool GodisnjaPretplata { get; set; }
        public int TipPaketaID { get; set; }
        public string KorisnikID { get; set; } = string.Empty;
        public List<SelectListItem> TipPaketaList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> KorisnikList { get; set; } = new List<SelectListItem>();

        public IEnumerable<TipPaketa> TipoviPaketa { get; set; }

        internal class PaketKorisnika : Models.PaketKorisnika
        {
            public bool godisnjaPretplata { get; set; }
            public int tipPaketaID { get; set; }
            public string korisnikID { get; set; }
        }
    }
}
