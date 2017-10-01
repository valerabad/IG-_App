using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD_App.Models
{
    public class Product
    {               
        public int ID { get; set; }
        public string Title { get; set; }
     
        public virtual ICollection<Order_Product> Order_Product { get; set; }        
        public virtual ICollection<Order> Orders { get; set; }
    }
}