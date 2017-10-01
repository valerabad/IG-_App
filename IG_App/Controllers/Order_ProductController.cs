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
    public class Order_ProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Order_Product
        public ActionResult Index()
        {
            return View(db.Order_Product.ToList());
        }

        // GET: Order_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Product order_Product = db.Order_Product.Find(id);
            if (order_Product == null)
            {
                return HttpNotFound();
            }
            return View(order_Product);
        }

        // GET: Order_Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order_Product/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Price,Count,Total")] Order_Product order_Product)
        {
            if (ModelState.IsValid)
            {
                db.Order_Product.Add(order_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_Product);
        }

        // GET: Order_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Product order_Product = db.Order_Product.Find(id);
            if (order_Product == null)
            {
                return HttpNotFound();
            }
            return View(order_Product);
        }

        // POST: Order_Product/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Price,Count,Total")] Order_Product order_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_Product);
        }

        // GET: Order_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Product order_Product = db.Order_Product.Find(id);
            if (order_Product == null)
            {
                return HttpNotFound();
            }
            return View(order_Product);
        }

        // POST: Order_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Product order_Product = db.Order_Product.Find(id);
            db.Order_Product.Remove(order_Product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
