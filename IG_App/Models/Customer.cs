using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD_App.Models
{
    public enum Categories { vip, top, middle, simple}
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Categories Categorry { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}