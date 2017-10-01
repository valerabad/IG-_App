using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD_App.Models
{
    public class Order_Product
    {
        public int? ID { get; set; }
        public int? Price { get; set; }
        public int? Count { get; set; }
        public int? Total { get; set; }        

        public virtual Order Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}