using BusinessObject.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RazorPages.ViewModels;
using Service.Implementation;
using Service.Interface;
using System.Security.Principal;

namespace RazorPages.Pages.CustomerPage
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Customer")
            {
                var loggedInAccountJson = HttpContext.Session.GetString("User");
                var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);

                if (loggedInAccount != null)
                {
                    var user = userService.GetUserByID(loggedInAccount.UserId);
                    User = user;
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                userService.UpdateUsersAccount(User);
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
            return userService.GetUserByID(id) != null;
        }

    }
}
