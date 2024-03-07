using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using System.Security.Principal;
using Service.Interface;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageUser
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService userService;

        public DetailsModel()
        {
            userService = new UserService();
        }

      public User User { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                User user = userService.GetUserByID((int)id);
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
    }
}
