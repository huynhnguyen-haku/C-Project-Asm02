using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Car
    {
        public Car()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CarId { get; set; }
        public int CategoryId { get; set; }
        public string CarName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public byte? CarStatus { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
