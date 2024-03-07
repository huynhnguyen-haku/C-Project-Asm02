using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Service.Implementation;
using Service.Interface;

namespace RazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService userService;

        public RegisterModel()
        {
            userService = new UserService();
        }

        [BindProperty]
        public User User { get; set; } = new User();

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xác nhận mật khẩu
                if (User.Password != ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
                    return Page();
                }

                // Tạo User từ dữ liệu đăng ký
                var newUser = new User
                {
                    Email = User.Email,
                    UserName = User.UserName,
                    Password = User.Password,
                    Birthday = User.Birthday,
                    Role = "Customer" // Mặc định là Customer khi đăng ký
                };

                // Thực hiện đăng ký
                try
                {
                    userService.CreateUserAccounts(newUser);
                    return RedirectToPage("/Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Registration failed. {ex.Message}");
                    return Page();
                }
            }

            return RedirectToPage("/Login");
        }
    }
}
