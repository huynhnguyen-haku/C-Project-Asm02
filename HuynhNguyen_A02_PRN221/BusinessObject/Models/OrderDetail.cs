﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
