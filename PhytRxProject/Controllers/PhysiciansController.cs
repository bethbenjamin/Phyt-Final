using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhytRxProject.Models;

namespace PhytRxProject.Controllers
{
    public class PhysiciansController : Controller
    {
        private Entities db = new Entities();

        // GET: Physicians
        public ActionResult Index()
        {
            var physicians = db.Physicians.Include(p => p.Log).Include(p => p.RX);
            return View(physicians.ToList());
        }

        // GET: Physicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

        // GET: Physicians/Create
        public ActionResult Create()
        {
            ViewBag.LogID = new SelectList(db.Logs, "LogID", "ComTxt");
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName");
            return View();
        }

        // POST: Physicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhID,PhName,PhPic,PID,LogID,RxID")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                db.Physicians.Add(physician);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LogID = new SelectList(db.Logs, "LogID", "ComTxt", physician.LogID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", physician.RxID);
            return View(physician);
        }

        // GET: Physicians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            ViewBag.LogID = new SelectList(db.Logs, "LogID", "ComTxt", physician.LogID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", physician.RxID);
            return View(physician);
        }

        // POST: Physicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhID,PhName,PhPic,PID,LogID,RxID")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LogID = new SelectList(db.Logs, "LogID", "ComTxt", physician.LogID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", physician.RxID);
            return View(physician);
        }

        // GET: Physicians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

        // POST: Physicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Physician physician = db.Physicians.Find(id);
            db.Physicians.Remove(physician);
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
