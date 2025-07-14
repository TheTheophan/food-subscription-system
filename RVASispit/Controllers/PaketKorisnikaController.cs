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
                    Text = tp.nazivPaketa,
                })
                .ToListAsync();

            var viewModel = new PaketKorisnikaCreateViewModel
            {
                TipPaketaList = tipPaketaList
                
            };
            return View(viewModel);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
