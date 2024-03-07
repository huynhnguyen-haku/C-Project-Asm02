using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.ManagerPage.ManageCar
{
    public class DetailsModel : PageModel
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public DetailsModel()
        {
            carService = new CarService();
            categoryService = new CategoryService();
        }

        public Car Car { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Manager")
            {
                if (id == null)
                {
                    return NotFound();
                }

                Car car = carService.GetCarByID((int)id);
                if (car == null)
                {
                    return NotFound();
                }
                else
                {
                    Car = car;
                    Category = categoryService.GetCategoryById(Car.CategoryId);
                }
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
