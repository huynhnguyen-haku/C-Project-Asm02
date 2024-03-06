using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service.Interface;
using Service.Implementation;
using Newtonsoft.Json;
using System.Security.Principal;

namespace RazorPages.Pages.UserPage
{
    public class ListProfileModel : PageModel
    {
        private readonly IUserService userService;

        public ListProfileModel()
        {
            userService = new UserService();
        }

        public IList<User> User { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Customer")
            {
                var loggedInAccountJson = HttpContext.Session.GetString("User");
                var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);
                User = userService.GetUserList().Where(x => x.UserId.Equals(loggedInAccount.UserId)).ToList();
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
