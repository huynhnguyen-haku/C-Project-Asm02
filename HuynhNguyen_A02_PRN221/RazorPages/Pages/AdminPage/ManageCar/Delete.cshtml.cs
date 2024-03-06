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
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var car = carService.GetCarById((int)id);

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
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carService.GetCarById((int)id);
            Car = carService.RemoveCar(car);
            return RedirectToPage("./Index");
        }
    }
}
