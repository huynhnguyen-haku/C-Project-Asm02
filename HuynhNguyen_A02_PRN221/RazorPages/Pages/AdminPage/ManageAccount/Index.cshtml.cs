using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service.Implementation;
using System.Security.Principal;
using Service.Interface;
using Repository.Implementation;

namespace RazorPages.Pages.AdminPage.ManageAccount
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IndexModel()
        {
            userService = new UserService();
        }

        public IList<User> User { get;set; } = default!;

        [BindProperty] public string SearchValue { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                SearchValue = "";
                User = userService.GetUserList().ToList();
                return Page();
            }
        return RedirectToPage("/Login");
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                User = userService.GetUserList();
                return Page();
            }

            var search = SearchValue.ToUpper().Trim();
            User = userService.GetUserList()
                .Where(O => O.UserName.ToUpper().Trim().Contains(search)
                            || O.UserName.ToUpper().Trim().Contains(search)
                            || O.City.ToUpper().Trim().Contains(search)
                            || O.Country.ToUpper().Trim().Contains(search)
                            || O.Email.ToUpper().Trim().Contains(search)
                ).ToList();
            return Page();
        }
    }
}
