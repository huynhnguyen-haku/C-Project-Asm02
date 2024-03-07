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

namespace RazorPages.Pages.AdminPage.ManageCar
{
    public class DeleteModel : PageModel
    {
        private readonly ICarService carService;

        public DeleteModel()
        {
            carService = new CarService();
        }

        [BindProperty]
      public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || carService.GetCarsList() == null)
            {
                return NotFound();
            }

            var car = carService.GetCarByID((int)id);

            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                Car = car;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || carService.GetCarsList() == null)
            {
                return NotFound();
            }
            var car = carService.GetCarByID((int)id);

            if (car != null)
            {
                carService.DeleteCar((int)id);
                return RedirectToPage("./Index");
            }
            return NotFound();
        }
    }
}
