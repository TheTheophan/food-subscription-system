using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RVASispit.Data;
using RVASispit.Models;

namespace RVASispit.Controllers
{
    public class TipPaketaController : Controller
    {

        private readonly ApplicationDbContext _context;
        public TipPaketaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateTipPaketa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTipPaketa(TipPaketa model)
        {
            if (ModelState.IsValid)
            {
                var tipPaketa = new TipPaketa()
                {
                    cenaGodisnjePretplate = model.cenaGodisnjePretplate,
                    cenaMesecnePretplate = model.cenaMesecnePretplate,
                    opisPaketa = model.opisPaketa,
                    nazivPaketa = model.nazivPaketa,
                    cenaRezervacije = model.cenaRezervacije
                };

                _context.TipPaketas.Add(tipPaketa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View("Create", model);

        }


        public async Task<IActionResult> Index()
        {
            var tipPaketaList = await _context.TipPaketas.ToListAsync();
            return View(tipPaketaList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tipPaketa = await _context.TipPaketas.FindAsync(id);
            if (tipPaketa == null)
            {
                return NotFound();
            }
            var TipPaketa = new TipPaketa()
            {
                Id = tipPaketa.Id,
                cenaGodisnjePretplate = tipPaketa.cenaGodisnjePretplate,
                cenaMesecnePretplate = tipPaketa.cenaMesecnePretplate,
                opisPaketa = tipPaketa.opisPaketa,
                nazivPaketa = tipPaketa.nazivPaketa,
                cenaRezervacije = tipPaketa.cenaRezervacije
            };
            return View(TipPaketa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipPaketa model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tipPaketa = await _context.TipPaketas.FindAsync(model.Id);
                if (tipPaketa == null)
                {
                    return NotFound();
                }
                tipPaketa.cenaGodisnjePretplate = model.cenaGodisnjePretplate;
                tipPaketa.cenaMesecnePretplate = model.cenaMesecnePretplate;
                tipPaketa.opisPaketa = model.opisPaketa;
                tipPaketa.nazivPaketa = model.nazivPaketa;
                tipPaketa.cenaRezervacije = model.cenaRezervacije;
                _context.TipPaketas.Update(tipPaketa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var tipPaketa = await _context.TipPaketas.FindAsync(id);
            if (tipPaketa == null)
            {
                return NotFound();
            }
            _context.TipPaketas.Remove(tipPaketa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var tipPaketa = await _context.TipPaketas.FindAsync(id);
            if (tipPaketa == null)
            {
                return NotFound();
            }

            var TipPaketa = new TipPaketa()
            {
                Id = tipPaketa.Id,
                cenaGodisnjePretplate = tipPaketa.cenaGodisnjePretplate,
                cenaMesecnePretplate = tipPaketa.cenaMesecnePretplate,
                opisPaketa = tipPaketa.opisPaketa,
                nazivPaketa = tipPaketa.nazivPaketa,
                cenaRezervacije = tipPaketa.cenaRezervacije
            };

            return View(TipPaketa);

        }
    }
}
