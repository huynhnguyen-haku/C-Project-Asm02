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
    public class OrderDetailModel : PageModel
    {
        private readonly IUserService userService = new UserService();
        private readonly IOrderDetailService orderDetailService = new OrderDetailService();
        private readonly ICarService carService = new CarService();

        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetail { get;set; }

        public IActionResult OnGet(string id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Customer")
            {
                var loggedInAccountJson = HttpContext.Session.GetString("User");
                var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);

                if (loggedInAccount != null)
                {
                    var orderId = int.Parse(id);
                    OrderDetail = orderDetailService.GetOrderDetailByOrderId(orderId);

                    foreach (var item in OrderDetail)
                    {
                        item.Car = carService.GetCarByID(item.CarId);
                    }

                    Total = OrderDetail.Sum(i => i.UnitPrice * i.Quantity);
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }
    }
}
