using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime DateTime { get; set; }

        public List<ProductOrder> ProductOrder { get; set; }
    }
}
