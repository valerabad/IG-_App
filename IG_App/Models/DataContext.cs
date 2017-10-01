using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BD_App.Models
{
    public class DataContext : DbContext
    {
       
        // наполняем бд в конструкторе
        public DataContext() : base("DataContext")
        {
          
        }

        public DbSet<Customer> Customer { get; set; }        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Product> Order_Product { get; set; }
    }
}