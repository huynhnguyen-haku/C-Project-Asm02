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
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.AdminPage.ManageUser
{
    public class EditModel : PageModel
    {
        private readonly IUserService userService;

        public EditModel()
        {
            userService = new UserService();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }
                var user = userService.GetUserByID((int)id);
                if (user == null)
                {
                    return NotFound();
                }
                User = user;
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
                User = userService.UpdateUsersAccount(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
            }
            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return userService.GetUserByID((int)id) != null;
        }
    }
}
