using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RVASispit.Data;
using Microsoft.AspNetCore.Identity;
using RVASispit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVASispit.Models.ViewModels;

namespace RVASispit.Controllers
{

    [Authorize]
    public class PaketKorisnikaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaketKorisnikaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var tipPaketaList = await _context.TipPaketas
                .Select(tp => new SelectListItem
                {
                    Value = tp.Id.ToString(),
                    Text = $"{tp.nazivPaketa} | Godišnja: {tp.cenaGodisnjePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Mesečna: {tp.cenaMesecnePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Rezervacija: {tp.cenaRezervacije.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Opis: {tp.opisPaketa}"
                })
                .ToListAsync();

            var viewModel = new PaketKorisnikaCreateViewModel
            {
                TipPaketaList = tipPaketaList
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaketKorisnikaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate TipPaketaList for redisplay
                model.TipPaketaList = await _context.TipPaketas
                    .Select(tp => new SelectListItem
                    {
                        Value = tp.Id.ToString(),
                        Text = $"{tp.nazivPaketa} | Godišnja: {tp.cenaGodisnjePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Mesečna: {tp.cenaMesecnePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Rezervacija: {tp.cenaRezervacije.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Opis: {tp.opisPaketa}"
                    })
                    .ToListAsync();
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            // Fetch the required related entities
            var tipPaketa = await _context.TipPaketas.FindAsync(model.TipPaketaID);
            var korisnik = await _userManager.FindByIdAsync(userId);

            if (tipPaketa == null || korisnik == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid package or user.");
                model.TipPaketaList = await _context.TipPaketas
                    .Select(tp => new SelectListItem
                    {
                        Value = tp.Id.ToString(),
                        Text = $"{tp.nazivPaketa} | Godišnja: {tp.cenaGodisnjePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Mesečna: {tp.cenaMesecnePretplate.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Rezervacija: {tp.cenaRezervacije.ToString("C", new System.Globalization.CultureInfo("sr-RS"))} | Opis: {tp.opisPaketa}"
                    })
                    .ToListAsync();
                return View(model);
            }

            var paketKorisnika = new PaketKorisnika
            {
                godisnjaPretplata = model.GodisnjaPretplata,
                tipPaketaID = model.TipPaketaID,
                korisnikID = userId,
                tipPaketa = tipPaketa, 
                korisnik = korisnik   
            };

            _context.PaketiKorisnikas.Add(paketKorisnika);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
