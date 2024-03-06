using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.UserPage
{
    public class ShopViewModel : PageModel
    {
        private readonly ICarService carService = new CarService();

        public IList<Car> Car { get; set; }
       

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Customer")
            {
                Car = carService.GetCarList();
                return Page();
            }
            return RedirectToPage("/Login");
        }

    }
}
