using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class GroupContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupContacts
        public ActionResult Index()
        {
            return View(db.AspNetGroupContacts.ToList());
        }

        // GET: GroupContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(id);
            if (aspNetGroupContact == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupContact);
        }

        // GET: GroupContacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,ContactId")] AspNetGroupContact aspNetGroupContact)
        {
            if (ModelState.IsValid)
            {
                db.AspNetGroupContacts.Add(aspNetGroupContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetGroupContact);
        }

        // GET: GroupContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(id);
            if (aspNetGroupContact == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupContact);
        }

        // POST: GroupContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,ContactId")] AspNetGroupContact aspNetGroupContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetGroupContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetGroupContact);
        }

        // GET: GroupContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(id);
            if (aspNetGroupContact == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupContact);
        }

        // POST: GroupContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(id);
            db.AspNetGroupContacts.Remove(aspNetGroupContact);
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
