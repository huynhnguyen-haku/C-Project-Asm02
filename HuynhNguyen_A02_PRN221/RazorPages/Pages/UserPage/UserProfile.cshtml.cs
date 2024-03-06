using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.ViewModels;
using Service.Implementation;
using Service.Interface;
using System.Security.Principal;

namespace RazorPages.Pages.UserPage
{
    public class UserProfileModel : PageModel
    {
        private readonly IUserService userService;

        public UserProfileModel()
        {
            userService = new UserService();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (HttpContext.Session.GetString("Role") == "Customer")
            {
                if (id == null)
                {
                    return NotFound();
                }
                var account = userService.GetUserById(id);
                if (account == null)
                {
                    return NotFound();
                }
                User = account;
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
                userService.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./ShopView");
        }

        private bool UserExists(int id)
        {
            return userService.GetUserById(id) != null;
        }

    }
}
