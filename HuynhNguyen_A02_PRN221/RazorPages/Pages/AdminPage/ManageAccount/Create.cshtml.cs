
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Models;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.AdminPage.ManageAccount
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
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                User result = userService.CreateUser(User);
                if (result != null)
                {
                    ViewData["notification"] = "Email is existed";
                    return OnGet();
                }
                return RedirectToPage("./Index");
            }
            return RedirectToPage("/Login");
        }
    }
}
