using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataRepository.Models;
using bw01.Models;

namespace bw01.Controllers
{
    public class CrewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Crew
        public ActionResult Index()
        {
            return View(db.CrewMembers.ToList());
        }

        // GET: Crew/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            return View(crewMember);
        }

        // GET: Crew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CrewMemberID,Name,MobilePhone,Email")] CrewMember crewMember)
        {
            if (ModelState.IsValid)
            {
                db.CrewMembers.Add(crewMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crewMember);
        }

        // GET: Crew/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            return View(crewMember);
        }

        // POST: Crew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CrewMemberID,Name,MobilePhone,Email")] CrewMember crewMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crewMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crewMember);
        }

        // GET: Crew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrewMember crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return HttpNotFound();
            }
            return View(crewMember);
        }

        // POST: Crew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrewMember crewMember = db.CrewMembers.Find(id);
            db.CrewMembers.Remove(crewMember);
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
