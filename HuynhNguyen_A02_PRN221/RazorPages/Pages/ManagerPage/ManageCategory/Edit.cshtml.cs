using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using Humanizer.Localisation;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.ManagerPage.ManageCategory
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public EditModel()
        {
            categoryService = new CategoryService();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Manager")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var category = categoryService.GetCategoryById((int)id);
                if (category == null)
                {
                    return NotFound();
                }
                Category = category;
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
                categoryService.UpdateCategory(Category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(int id)
        {
            return categoryService.GetCategoryById((int)id) != null;
        }
    }
}
