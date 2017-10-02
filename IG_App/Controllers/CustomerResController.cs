using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD_App.Models;

namespace IG_App.Controllers
{
    public class CustomerResController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CustomerRes
        public ActionResult Index()
        {
            var resultTable = db.Database.SqlQuery<CustomerRes>(@"select CustomerID as ID, Name, Address, Categorry, SUM(po.total) as 'sum'
                     from Orders AS o join Order_Product as po 
	                    on o.ID = po.Orders_ID
	                    join Customers AS c
	                    on c.ID = o.CustomerID
	                    group by o.CustomerID, Name, Address, Categorry
	                    order by o.CustomerID");
            
            return View(resultTable.ToList());
        }

        
    }
}
