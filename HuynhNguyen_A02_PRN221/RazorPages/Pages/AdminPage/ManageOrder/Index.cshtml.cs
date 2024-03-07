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
using RazorPages.ViewModels;

namespace RazorPages.Pages.AdminPage.ManageOrder
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public IndexModel()
        {
            orderService = new OrderService();
            userService = new UserService();    
        }

        public IList<Order> Order { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                Order = orderService.GetAllOrders();
                HttpContext.Session.SetObjectAsJson("cart", null);
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
