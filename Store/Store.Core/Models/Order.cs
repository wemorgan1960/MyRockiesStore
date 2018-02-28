﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Models
{
    public class Order : BaseEntity
    {
        public Order() {
            this.OrderItems = new List<OrderItem>();
        }
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public  Nullable<DateTime> CompletedAt { get; set; }
        public string OrderStatus { get; set; }

        public string TotalOrder { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
