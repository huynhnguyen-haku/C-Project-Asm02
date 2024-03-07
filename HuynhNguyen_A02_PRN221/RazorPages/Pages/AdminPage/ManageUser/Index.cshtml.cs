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
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IndexModel()
        {
            userService = new UserService();
        }

        public IList<User> User { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        //public IActionResult OnGetAsync()
        //{
        //    if (HttpContext.Session.GetString("Role") == "Admin")
        //    {
        //        User = userService.GetUsersList().Where(a => a.Role == "Customer").ToList();
        //        return Page();
        //    }
        //    return RedirectToPage("/Login");
        //}

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                // Get the list of users
                var users = userService.GetUsersList().Where(a => a.Role == "Customer");

                // Filter the users based on the search value
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    users = users.Where(u => u.Email.Contains(SearchTerm) ||
                    u.City.Contains(SearchTerm) ||
                    u.Country.Contains(SearchTerm) ||
                    u.UserName.Contains(SearchTerm));
                }

                User = users.ToList();

                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}