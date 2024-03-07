using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.AdminPage.ManageCategory
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public CreateModel()
        {
            categoryService = new CategoryService();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return Page();
            }
            return RedirectToPage("/Login");
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Category = categoryService.CreateCategory(Category);
            return RedirectToPage("./Index");
        }
    }
}
