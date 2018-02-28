using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Models
{
    public class BasketItem:BaseEntity
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quanity { get; set; }
        public decimal Price { get; set; }
        public decimal Shipping { get; set; }
    }
}
