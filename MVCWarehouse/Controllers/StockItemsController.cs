﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWarehouse.DataAccess;
using MVCWarehouse.Models;
using MVCWarehouse.Repositories;
// this is a comment
namespace MVCWarehouse.Controllers
{
    public class StockItemsController : Controller
    {
        private StoreContext db = new StoreContext();
        ItemRepository repo = new ItemRepository();

        public ActionResult Redirect()
        {

            return RedirectToAction("Index", "Home");
        }
        
        // GET: StockItems
        public ActionResult Index(string Name = "", double Price = 0, int ArticleNumber = 0)
        {
            if (ArticleNumber > 0)
                return View(repo.GetItemByArtNr(ArticleNumber));
            if (Price > 0)
                return View(repo.GetItemByPrice(Price));
            else if (Name == "")
                return View(db.Items.ToList());

            else
                return View(repo.GetItemByName(Name));
        }

//        public ActionResult Index(string Name = "", double Price = 0)
//        {
//            var stockItem =
//                from n in repo.GetItemByName(Name)
//                where Name == n.Name || Price == n.Price
//                select n;
//                View(GetItemByBoth(Name, Price));
//        }


        public ActionResult PIndex(double Price)
        {
                return View(repo.GetItemByPrice(Price));
        }

        public ActionResult GetItemByName(string name = "")
        {
            IEnumerable<StockItem> item = repo.GetItemByName(name);

            return View(stockItem);
        }


        // GET: StockItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // GET: StockItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleNumber,Name,Price,ShelfPosition,Quantity,Description")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(stockItem);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(stockItem);
        }

        // GET: StockItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleNumber,Name,Price,ShelfPosition,Quantity,Description")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.Items.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockItem stockItem = db.Items.Find(id);
            db.Items.Remove(stockItem);
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

        public StockItem stockItem { get; set; }
    }
}
