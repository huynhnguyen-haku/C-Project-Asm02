using System;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Service.Interface;
using Service.Implementation;
using Service;
using Newtonsoft.Json;

namespace RazorPages.Pages.CustomerPage
{
    public class CartModel : PageModel
    {
        public List<CartItem> Cart { get; set; }
        public decimal Total { get; set; }
        [BindProperty]
        public DateTime ShipDate { get; set; }

        private readonly ICarService carService = new CarService();
        private readonly IOrderService orderService = new OrderService();
        private readonly IOrderDetailService orderDetailService = new OrderDetailService();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Customer")
            {
                Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                if (Cart == null) // Not found old cart
                {
                    Cart = new List<CartItem>(); // create new
                }
                Total = Cart.Sum(i => i.Item.UnitPrice * i.Quantity);
                return Page();
            }
            return RedirectToPage("/Login");
        }

        public IActionResult OnGetBuyNow(string id)
        {
            Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart"); // try to get old cart from session
            if (Cart == null) // Not found old cart
            {
                Cart = new List<CartItem>(); // create new
                var carItem = carService.GetCarByID(int.Parse(id));
                Cart.Add(new CartItem
                {
                    Item = carItem,
                    Quantity = 1
                });
                HttpContext.Session.SetObjectAsJson("cart", Cart); // set to session
            }
            else // Already have cart
            {
                int index = CheckExist(Cart, int.Parse(id)); // Check if already have item in cart
                if (index == -1) // Not see? add new with quantity 1
                {
                    var carItem = carService.GetCarByID(int.Parse(id));
                    Cart.Add(new CartItem
                    {
                        Item = carItem,
                        Quantity = 1
                    });
                }
                else
                {
                    Cart[index].Quantity++; // Already have -> add 1 to cart
                }

                HttpContext.Session.SetObjectAsJson("cart", Cart); // set to session
            }

            return RedirectToPage("Cart"); // Load Page
        }
        public IActionResult OnPostUpdate(int[] quantities)
        {
            Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            for (var i = 0; i < Cart.Count; i++)
            {
                Cart[i].Quantity = quantities[i];
            }

            HttpContext.Session.SetObjectAsJson("cart", Cart);
            return RedirectToPage("Cart"); // Reload
        }

        public IActionResult OnGetDelete(string id)
        {
            Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            int index = CheckExist(Cart, int.Parse(id));
            Cart.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("cart", Cart);
            return RedirectToPage("Cart");
        }
        
        public IActionResult OnPostCheckout()
        {

            Cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            if (Cart == null || Cart.Count == 0)
            {
                ModelState.AddModelError(String.Empty, "You do not have any item");
                return Page();
            }

            var loggedInAccountJson = HttpContext.Session.GetString("User");
            var loggedInAccount = JsonConvert.DeserializeObject<User>(loggedInAccountJson);

            // CHECK QUANTITY
            foreach (var orderDetail in Cart)
            {
                // Check Quantity
                var currentCar = carService.GetCarByID(orderDetail.Item.CarId);
                if (currentCar.UnitsInStock < orderDetail.Quantity)
                {
                    ModelState.AddModelError(String.Empty, currentCar.CarName + " Only have "+ currentCar.UnitsInStock+ " left!");
                    return Page();
                }
            }
            
            // CREATE ORDER
            Total = Cart.Sum(i => i.Item.UnitPrice * i.Quantity);
            var orderId = orderService.AddOrder(loggedInAccount.UserId, ShipDate, Total.ToString(),
                "Shipping",
                out var message);
            // Check Error
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(String.Empty, message);
                return Page();
            }

            // Create detail
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var orderDetail in Cart)
            {

                orderDetails.Add(new OrderDetail()
                {
                    OrderId = orderId,
                    CarId = orderDetail.Item.CarId,
                    Discount = 0,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.Item.UnitPrice,
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
                throw exception;
            }
            
            Cart.Clear();
            HttpContext.Session.SetObjectAsJson("cart", Cart);
            return RedirectToPage("./OrderHistory");
        }
        
        private int CheckExist(List<CartItem> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Item.CarId == id)
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
