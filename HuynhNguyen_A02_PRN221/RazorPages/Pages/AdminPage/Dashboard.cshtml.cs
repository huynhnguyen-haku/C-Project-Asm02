using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages.AdminPage
{
    public class DashboardModel : PageModel
    {
        // khai báo cho từng thành phần
        private readonly IUserService userService;
        private readonly ICarService carService;
        //private readonly IOrderService orderService;

        // bên Razor Page
        public int NumberOfUserAccounts { get; private set; }
        public int NumberOfCars { get; private set; }
        //public int NumberOfOrders { get; private set; }

        public DashboardModel()
        {
            userService = new UserService();
            carService = new CarService();
            //orderService = new OrderService();
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                // gọi các phương thức cảu Service của từng thành phần
                NumberOfUserAccounts = userService.GetNumberOfUserAccounts();
                NumberOfCars = carService.GetNumberOfCars();
                //NumberOfOrders = orderService.GetNumberOfOrders();
            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}
