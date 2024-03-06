using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Implementation;
using Service.Interface;
using System.Security.Principal;

namespace RazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;

        public LoginModel()
        {
            userService = new UserService();
        }
        [BindProperty] public User Account { get; set; }

        public IActionResult OnPostLogin()
        {
            User account = userService.GetUserByEmailAndPassword(Account.Email, Account.Password);
            if (account == null)
            {
                ViewData["notification"] = "Email or Password is wrong!";
                return Page();
            }

            else if (account.Role == "Admin")
            {
                //Login for admin
                HttpContext.Session.SetString("Role", account.Role);
                HttpContext.Session.SetString("Admin", JsonConvert.SerializeObject(account));
                if (HttpContext.Session.GetString("Role") == "Admin")
                {
                    return RedirectToPage("/AdminPage/Dashboard");
                }
            }
            else if (account.Role == "Customer")
            {
                //Login for customer
                HttpContext.Session.SetString("Role", account.Role);
                HttpContext.Session.SetString("Customer", JsonConvert.SerializeObject(account));
                if (HttpContext.Session.GetString("Role") == "Customer")
                {
                    return RedirectToPage("/UserPage/ShopView");
                }
            }
            else if (account.Role == "Manager")
            {
                //Login for customer
                HttpContext.Session.SetString("Role", account.Role);
                HttpContext.Session.SetString("Manager", JsonConvert.SerializeObject(account));
                if (HttpContext.Session.GetString("Role") == "Manager")
                {
                    return RedirectToPage("/ManagerPage/ManageCar/Index");
                }
            }
            ViewData["notification"] = "You do not have permission to do this function!";
            return Page();
        }
        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}