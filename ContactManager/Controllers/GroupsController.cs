using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;

namespace ContactManager.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AspNetGroups
        public ActionResult Index()
        {
            string queryString = "SELECT * "
                            + "FROM dbo.AspNetGroups "
                            + "WHERE OwnerID = '" + User.Identity.GetUserId() + "'";
            if (User.IsInRole("Moderator") || User.IsInRole("Admin"))
                queryString = "SELECT * "
                            + "FROM dbo.AspNetGroups";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            List<AspNetGroup> columnData = new List<AspNetGroup>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        columnData.Add(new AspNetGroup(reader));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            db.AspNetGroups.ToList();
            return View(columnData);
        }

        // GET: AspNetGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,UserId,GroupName,StatusMessage")] AspNetGroup aspNetGroup)
        {
            if (ModelState.IsValid)
            {
                aspNetGroup.UserId = User.Identity.GetUserId();
                db.AspNetGroups.Add(aspNetGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // POST: AspNetGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,UserId,GroupName,StatusMessage")] AspNetGroup aspNetGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetGroup);
        }

        // GET: AspNetGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            if (aspNetGroup == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroup);
        }

        // POST: AspNetGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetGroup aspNetGroup = db.AspNetGroups.Find(id);
            db.AspNetGroups.Remove(aspNetGroup);
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
