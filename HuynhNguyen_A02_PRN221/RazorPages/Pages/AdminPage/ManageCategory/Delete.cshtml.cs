using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using Humanizer.Localisation;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageCategory
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public DeleteModel()
        {
            categoryService = new CategoryService();
        }

        [BindProperty]
      public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
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
                else
                {
                    Category = category;
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
            categoryService.DeleteCategory((int)id);
            return RedirectToPage("./Index");
        }
    }
}
