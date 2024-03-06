using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService userService;
        private readonly ICarService carService;

        public DashboardModel()
        {
            userService = new UserService();
            carService = new CarService();
        }

        public int NumberOfUsers { get; private set; }
        public int NumberOfCars { get; private set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                NumberOfUsers = userService.GetUserCount();
                NumberOfCars = carService.GetCarCount();
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
