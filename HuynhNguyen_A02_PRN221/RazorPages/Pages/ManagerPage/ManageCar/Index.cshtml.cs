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

namespace RazorPages.Pages.ManagerPage.ManageCar
{
    public class IndexModel : PageModel
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public IndexModel()
        {
            carService = new CarService();
            categoryService = new CategoryService();
        }

        public IList<Car> Car { get;set; } = default!;
        [BindProperty] public string SearchValue { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Manager")
            {
                SearchValue = "";
                Car = carService.GetCarList();
                return Page();
            }
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                Car = carService.GetCarList();
                return Page();
            }

            var search = SearchValue.ToUpper().Trim();
            Car = carService.GetCarList()
                .Where(O => O.CarName.ToUpper().Trim().Contains(search)
                            || O.Category.CategoryName.ToUpper().Trim().Contains(search)
                            || O.Description.ToUpper().Trim().Contains(search)
                ).ToList();
            return Page();
        }
    }
}
