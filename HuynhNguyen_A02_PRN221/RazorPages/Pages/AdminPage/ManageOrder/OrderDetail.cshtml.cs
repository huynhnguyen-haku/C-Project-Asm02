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

namespace RazorPages.Pages.AdminPage.ManageOrder
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailService orderDetailService;
        private readonly ICarService carService;

        public OrderDetailModel()
        {
            orderDetailService = new OrderDetailService();
            carService = new CarService();
        }

        public decimal Total { get; set;}
        public List<OrderDetail> OrderDetail { get;set; } = default!;

        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
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
            return RedirectToPage("/Index");
        }
    }
}
