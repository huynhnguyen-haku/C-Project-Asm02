using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Service;
using Service.Implementation;

namespace RazorPages.Pages.AdminPage.ManageOrder
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService orderService;

        public DeleteModel()
        {
            orderService = new OrderService();
        }

        [BindProperty]
      public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if(id == null)
                {
                    return NotFound();
                }
                Order = orderService.GetOrderById((int)id);
                if(Order == null)
                {
                    return NotFound();
                }
                return Page();
            }
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Order = orderService.GetOrderById((int)id);

            if (Order != null)
            {
                orderService.DeleteOrder((int)id);
            }
            return RedirectToPage("./Index");
        }
    }
}
