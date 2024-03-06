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

namespace RazorPages.Pages.UserPage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IUserService userService = new UserService();
        private readonly IOrderService orderService = new OrderService();

        public List<Order> Order { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Customer" && HttpContext.Session.GetString("User") != null)
            {
                var userId = HttpContext.Session.GetString("User");

                // Kiểm tra xem userId có giá trị hợp lệ không
                if (!string.IsNullOrEmpty(userId))
                {
                    int id = int.Parse(userId);
                    var customer = userService.GetUserById(id);

                    if (customer != null)
                    {
                        Order = orderService.GetOrderByUser(id);
                        return Page();
                    }
                }
            }
            return RedirectToPage("/Login");
        }
    }

}
