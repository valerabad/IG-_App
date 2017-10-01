using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD_App.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> CustomerID { get; set; }

        public virtual Customer Customers { get; set; }
       
        public virtual ICollection<Order_Product> Order_Product { get; set; }
       
        public virtual ICollection<Product> Products { get; set; }
    }
}