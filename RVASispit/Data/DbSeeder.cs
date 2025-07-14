using Microsoft.AspNetCore.Identity;
using RVASispit.Constants;
using RVASispit.Models;

namespace RVASispit.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var user = new ApplicationUser
            {
                UserName = "admin@raf.rs",
                Email = "admin@raf.rs",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };

            var userInDB = await userManager.FindByEmailAsync(user.Email);

            if (userInDB == null)
            {
                await userManager.CreateAsync(user, "Cet123$");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            }



        }
        public static async Task SeedTipPaketaAsync(IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var tipPaketaList = new List<TipPaketa>
            {
                new TipPaketa
                {
                    nazivPaketa = "Za Mene",
                    opisPaketa = "Farmit paket  Za Mene  Obezbedi malu gajbicu povrca iz zajednicke baste u kojoj sadimo 15 razlicitih kultura.    Zajednicka basta  Prosecno 6kg domaceg povrca svake nedelje, idealna kolicina za 2 osobe  Ukupno 15 kultura sezonskog povrca tokom sezone  Minimum 20 dostava od aprila do novembra, na kucni prag  Opcija promene adrese ili doniranja gajbice  Pracenje statusa kultura i slike baste preko aplikacije  Mogucnost posete baste",
                    cenaGodisnjePretplate = 39000,
                    cenaMesecnePretplate = 4900,
                    cenaRezervacije = 5000
                },
                new TipPaketa
                {
                    nazivPaketa = "Za Nas",
                    opisPaketa = "Farmit paket  Za Nas  Obezbedi veliku gajbicu povrca iz zajednicke baste u kojoj sadimo 15 razlicitih kultura.    Zajednicka basta  Prosecno 10kg domaceg povrca svake nedelje, idealna kolicina za cetvoroclanu porodicu  Ukupno 15 kultura sezonskog povrca tokom sezone  Minimum 20 dostava od aprila do novembra, na kucni prag  Opcija promene adrese ili doniranja gajbice  Pracenje statusa kultura i slike baste preko aplikacije  Mogucnost posete baste",
                    cenaGodisnjePretplate = 49000,
                    cenaMesecnePretplate = 5900,
                    cenaRezervacije = 7500
                },
                new TipPaketa
                {
                    nazivPaketa = "Moja Bašta",
                    opisPaketa = "Farmit paket  Moja Basta  Izaberi 10 omiljenih kultura, zasadi sopstvenu bastu i uzivaj u omiljenom povrcu.    Ogradena basta sa personalizovanom tablom  Prosecno 10kg domaceg povrca svake nedelje, idealna kolicina za cetvoroclanu porodicu  Sam/a biras kulture koje zelis da zasadis  Minimum 20 dostava od aprila do novembra, na kucni prag  Opcija promene adrese ili doniranja gajbice  Pracenje statusa kultura i slike baste preko aplikacije  Mogucnost posete baste  Poklon iznenadenja tokom sezone",
                    cenaGodisnjePretplate = 64000,
                    cenaMesecnePretplate = 7400,
                    cenaRezervacije = 10000
                }
            };

            foreach (var tipPaketa in tipPaketaList)
            {
                if (!context.TipPaketas.Any(tp => tp.nazivPaketa == tipPaketa.nazivPaketa))
                {
                    context.TipPaketas.Add(tipPaketa);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
