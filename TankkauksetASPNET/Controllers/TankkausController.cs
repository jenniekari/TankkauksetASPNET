using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TankkauksetASPNET.Models;

namespace TankkauksetASPNET.Controllers
{
    public class TankkausController : Controller
    {
        TankkauksetEntities db = new TankkauksetEntities();

        // GET: Tankkaus
        public ActionResult Index()
        {
            return View(db.Tankkaus.ToList());
        }

        // GET: Tankkaus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tankkaus tankkaus = db.Tankkaus.Find(id);
            if (tankkaus == null)
            {
                return HttpNotFound();
            }
           /* Tankkaus lasku = db.Tankkaus.Find(id);
            ViewBag.Totaali = 100 * lasku.Mittarilukema / lasku.Ajomaara;
            return View(lasku);*/
            return View(tankkaus);
        }

        // GET: Tankkaus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tankkaus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TankkausId,Pvm,Litraa,Euroa,Reknro,Mittarilukema,Ajomaara,Keskikulutus")] Tankkaus tankkaus)
        {
            if (ModelState.IsValid)
            {
                db.Tankkaus.Add(tankkaus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tankkaus);
        }

        // GET: Tankkaus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tankkaus tankkaus = db.Tankkaus.Find(id);
            if (tankkaus == null)
            {
                return HttpNotFound();
            }
            return View(tankkaus);
        }

        // POST: Tankkaus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TankkausId,Pvm,Litraa,Euroa,Reknro,Mittarilukema,Ajomaara,Keskikulutus")] Tankkaus tankkaus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tankkaus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tankkaus);
        }

        // GET: Tankkaus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tankkaus tankkaus = db.Tankkaus.Find(id);
            if (tankkaus == null)
            {
                return HttpNotFound();
            }
            return View(tankkaus);
        }

        // POST: Tankkaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tankkaus tankkaus = db.Tankkaus.Find(id);
            db.Tankkaus.Remove(tankkaus);
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
        public ActionResult YourAction()
        {
            //C# code here
            return View();
        }
    }
}
