using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.ManagerPage.ManageCar
{
    public class EditModel : PageModel
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public EditModel()
        {
            carService = new CarService();
            categoryService = new CategoryService();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Manager")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var car = carService.GetCarById((int)id);
                var category = categoryService.GetCategoryType();

                var categorySelectList = category.Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                }).ToList();

                if (car == null)
                {
                    return NotFound();
                }
                Car = car;
                ViewData["CategoryId"] = new SelectList(categorySelectList, "Value", "Text");
                return Page();
            }
            return RedirectToPage("/Login");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                carService.UpdateCar(Car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.CarId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
            return carService.GetCarById((int)id) != null;
        }
    }
}
