using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageUser
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService userService;

        public DeleteModel()
        {
            userService = new UserService(); ;
        }

        [BindProperty]
      public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || userService.GetUsersList() == null)
            {
                return NotFound();
            }

            var user = userService.GetUserByID((int)id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || userService.GetUsersList() == null)
            {
                return NotFound();
            }

            var user = userService.GetUserByID((int)id);

            if (user != null)
            {
                userService.DeleteUser((int)id);
                return RedirectToPage("./Index");
            }
            return NotFound();
        }
    }
}
