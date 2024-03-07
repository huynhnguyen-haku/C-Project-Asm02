using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Implementation;
using Newtonsoft.Json;

namespace RazorPages.Pages.CustomerPage
{
    public class ShopViewModel : PageModel
    {
        private readonly ICarService carService = new CarService();

        public IList<Car> Car { get; set; }
       
        public IActionResult OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Customer")
            {
                var loggedInAccountJson = HttpContext.Session.GetString("User");
                var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);

                if (loggedInAccount != null)
                {
                    Car = carService.GetCarsList();
                    return Page();
                }
            }

            return RedirectToPage("/Login");
        }
    }
}
