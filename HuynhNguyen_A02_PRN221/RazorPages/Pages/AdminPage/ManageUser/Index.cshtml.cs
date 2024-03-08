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
                SearchTerm = "";
                User = userService.GetUsersList();
                return Page();
            }
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostSearch()
        {
            if(string.IsNullOrEmpty(SearchTerm))
            {
                User = userService.GetUsersList();
                return Page();
            }
            var search = SearchTerm.ToLower().Trim();
            User = userService.GetUsersList().Where(a => a.UserName.ToLower().Contains(search)).ToList();
            return Page();
        }
    }
}