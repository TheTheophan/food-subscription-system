
using Microsoft.AspNetCore.Identity;

namespace RVASispit.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? imePrezime { get; set; }
        public DateTime datumKreiranjaKorisnika { get; set; } = DateTime.UtcNow;
        public string? adresaDostave { get; set; } = string.Empty;

        public List<PaketKorisnika> PaketiKorisnikas { get; set; }
    }
}
        
