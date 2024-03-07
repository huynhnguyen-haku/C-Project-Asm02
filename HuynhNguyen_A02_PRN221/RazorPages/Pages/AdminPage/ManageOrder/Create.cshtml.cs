using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Service;
using Service.Interface;
using Service.Implementation;
using RazorPages.ViewModels;

namespace RazorPages.Pages.AdminPage.ManageOrder
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IOrderDetailService orderDetailService;

        public CreateModel()
        {
            orderService = new OrderService();
            userService = new UserService();
            orderDetailService = new OrderDetailService();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                if (Cart == null)
                {
                    return RedirectToPage("./Index");
                }
                Order = new Order();
                Total = Cart.Sum(x => x.Item.UnitPrice * x.Quantity);
                Order.Total = Total;
                ViewData["UserId"] = new SelectList(userService.GetUsersList(), "UserId", "UserName");
                return Page();
            }
            return RedirectToPage("/Login");
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public decimal Total { get; set; } = default!;

        private List<CartItem> Cart { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            Total = Cart.Sum(x => x.Item.UnitPrice * x.Quantity);

            //Create Order
            var orderId = orderService.AddOrder(Order.UserId, Order.ShippedDate, Total.ToString(),
                Order.OrderStatus,
                out var message);

            //Check Error
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(String.Empty, message);
                return Page();
            }

            //Create Order Detail
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var orderDetail in Cart)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = orderId,
                    CarId = orderDetail.Item.CarId,
                    Discount = 0,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.Item.UnitPrice
                });
            }

            try
            {
                orderDetailService.AddOrderDetail(orderDetails);
            }
            catch (Exception exception)
            {
                orderService.DeleteOrder(orderId);
                ModelState.AddModelError(String.Empty, "Error when create details");
                return Page();
            }
            Cart.Clear();
            HttpContext.Session.SetObjectAsJson("cart", Cart);
            return RedirectToPage("./Index");
        }
    }
}
