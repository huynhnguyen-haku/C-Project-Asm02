using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.ViewModels;
using Service.Interface;
using Service.Implementation;
using Service;
using BusinessObject.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;

namespace RazorPages.Pages.CustomerPage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IUserService userService = new UserService();
        private readonly IOrderService orderService = new OrderService();

        public List<Order> Order { get; set; }

        public IActionResult OnGet()
        {

            {
                var role = HttpContext.Session.GetString("Role");

                if (role == "Customer")
                {
                    var loggedInAccountJson = HttpContext.Session.GetString("User");
                    var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);

                    if (loggedInAccount != null)
                    {
                        Order = orderService.GetOrderByUser(loggedInAccount.UserId);
                        return Page();
                    }
                }
                return RedirectToPage("/Login");
            }
        }
    }

}
