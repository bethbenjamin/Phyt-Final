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
    public class LogsController : Controller
    {
        private Entities db = new Entities();

        // GET: Logs
        public ActionResult Index(int pID)
        {

            var logs = db.Logs.Include(l => l.Exercis).Include(l => l.RX);
            ViewBag.PID = pID;

            return View(logs.ToList());
        }

        // GET: Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            ViewBag.ExID = new SelectList(db.Exercises, "ExID", "ExName");
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName");
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,PainNumb,DiffNumb,ComTxt,IsComplete,FullSet,PartialSet,PID,PhID,RxID,ExID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExID = new SelectList(db.Exercises, "ExID", "ExName", log.ExID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // GET: Logs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExID = new SelectList(db.Exercises, "ExID", "ExName", log.ExID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,PainNumb,DiffNumb,ComTxt,IsComplete,FullSet,PartialSet,PID,PhID,RxID,ExID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExID = new SelectList(db.Exercises, "ExID", "ExName", log.ExID);
            ViewBag.RxID = new SelectList(db.RXes, "RxID", "RxName", log.RxID);
            return View(log);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
