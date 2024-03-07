using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageUser
{
    public class CreateModel : PageModel
    {
        private readonly IUserService userService;

        public CreateModel()
        {
            userService = new UserService();
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
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            User result = userService.CreateUserAccounts(User);
            if (result != null)
            {
                ViewData["notification"] = "Email is existed";
                return OnGet();
            }
            return RedirectToPage("./Index");
        }
    }
}
