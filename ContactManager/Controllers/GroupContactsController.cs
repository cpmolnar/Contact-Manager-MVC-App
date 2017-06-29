using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using System.Data.Entity.Migrations;

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
        /*[HttpPost]
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
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int groupId, int[] contactIds)
        {
            if (ModelState.IsValid)
            {
                for (int i=0; i < contactIds.Length; i++)
                {
                    AspNetGroupContact item = new AspNetGroupContact(groupId, contactIds[i]);
                    db.AspNetGroupContacts.AddOrUpdate(item);
                }
                
                db.SaveChanges();
                return RedirectToAction("../Groups/Index");
            }

            return View();
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
        public ActionResult Delete(int? groupId, int contactId)
        {
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(groupId, contactId);
            if (aspNetGroupContact == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupContact);
        }

        // POST: GroupContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int groupId, int contactId)
        {
            AspNetGroupContact aspNetGroupContact = db.AspNetGroupContacts.Find(groupId, contactId);
            db.AspNetGroupContacts.Remove(aspNetGroupContact);
            db.SaveChanges();
            return RedirectToAction("../Groups/Index");
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
