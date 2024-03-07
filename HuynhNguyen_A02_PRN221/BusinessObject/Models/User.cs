using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
