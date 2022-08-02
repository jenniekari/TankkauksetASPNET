using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TankkauksetASPNET.Models;
using PagedList;

namespace TankkauksetASPNET.Controllers
{
    public class TankkausController : Controller
    {
        TankkauksetEntities db = new TankkauksetEntities();

        // GET: Tankkaus
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pagesize)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei virhettä sisäänkirjautuessa

                ViewBag.CurrentSort = sortOrder;
                //if-lause vb.pnsp jälkeen = Jos ensimmäinen lause on tosi ? toinen lause toteutuu : jos epätosi, niin tämä kolmas lause toteutuu
                ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

                //Hakufiltterin laitto muistiin
                if (searchString != null) //tarkistetaan onko käyttäjän antama arvo (esim. kirjain a tai sana villa) eri suuruinen kuin null
                {
                    page = 1; //jos a-kirjainta etitään, niin vie sivulle 1 kaikki tuotteet, jossa a-kirjain
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.currentFilter = searchString;

                var tankkaukset = from p in db.Tankkaus
                                  select p;

                if (!String.IsNullOrEmpty(searchString)) //Jos hakufiltteri on käytössä, niin käytetään sitä ja sen lisäksi lajitellaan tulokset
                {
                    switch (sortOrder)
                    {
                        case "Date_desc":
                            tankkaukset = tankkaukset.Where(p => p.Reknro.Contains(searchString)).OrderByDescending(p => p.Pvm);
                            break;
                        default:
                            tankkaukset = tankkaukset.Where(p => p.Reknro.Contains(searchString)).OrderBy(p => p.Pvm);
                            break;
                    }
                }
                else
                {
                    switch (sortOrder)
                    {
                        case "Date_desc":
                            tankkaukset = tankkaukset.OrderByDescending(p => p.Pvm);
                            break;
                        default:
                            tankkaukset = tankkaukset.OrderBy(p => p.Pvm);
                            break;
                    }
                };

                int pageSize = (pagesize ?? 10); //Tämä palauttaa sivukoon taikka jos pagesize on null, niin palauttaa koon 10 riviä per sivu
                int pageNumber = (page ?? 1); //int pageNumber on sivuparametrien arvojen asetus. Tämä palauttaa sivunumeron taikka jos page on null, niin palauttaa numeron yksi
                return View(tankkaukset.ToPagedList(pageNumber, pageSize));
                //return View(db.Tankkaus.ToList());
            }
        }

        // GET: Tankkaus/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei virhettä sisäänkirjautuessa

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
        }

        // GET: Tankkaus/Create
        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei virhettä sisäänkirjautuessa

                return View();
            }
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
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei virhettä sisäänkirjautuessa

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
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //ei virhettä sisäänkirjautuessa

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
    }
}
