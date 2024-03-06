using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageCar
{
    public class CreateModel : PageModel
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public CreateModel()
        {
            carService = new CarService();
            categoryService = new CategoryService();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                ViewData["CategoryId"] = new SelectList(categoryService.GetCategoryList(), "CategoryId", "CategoryName");
                return Page();
            }
            return RedirectToPage("/Login");
        }

        [BindProperty]
        public Car Car { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            carService.CreateCar(Car);
            return RedirectToPage("./Index");
        }
    }
}
