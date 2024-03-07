using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageOrder
{
    public class ShopViewModel : PageModel
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public ShopViewModel()
        {
            carService = new CarService();
            categoryService = new CategoryService();
        }

        public IList<Car> Car { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                Car = carService.GetCarsList();
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
