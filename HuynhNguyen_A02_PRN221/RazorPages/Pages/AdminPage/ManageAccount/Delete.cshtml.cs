using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.AdminPage.ManageAccount
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService userService;

        public DeleteModel()
        {
            userService = new UserService();
        }

        [BindProperty]
      public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                User user = userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    User = user;
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
            userService.DeleteUser((int)id);
            return RedirectToPage("./Index");
        }
    }
}
