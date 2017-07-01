using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace ContactManager.Controllers
{
    public class GroupUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        // GET: AspNetGroupUsers
        public ActionResult Index()
        {
            return View(db.AspNetGroupUsers.ToList());
        }

        // GET: AspNetGroupUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupUser aspNetGroupUser = db.AspNetGroupUsers.Find(id);
            if (aspNetGroupUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupUser);
        }

        // GET: AspNetGroupUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetGroupUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int groupId, string username)
        {
            if (ModelState.IsValid)
            {
                string queryString = "SELECT dbo.AspNetUsers.Id "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE dbo.AspNetUsers.UserName='" + username + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    AspNetGroupUser gu = new AspNetGroupUser
                    {
                        GroupId = groupId,
                        UserId = reader.GetString(0)
                    };
                    db.AspNetGroupUsers.Add(gu);
                    db.SaveChanges();
                    reader.Close();
                }
            }
            return RedirectToAction("../Groups/Index");
        }

        // GET: AspNetGroupUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetGroupUser aspNetGroupUser = db.AspNetGroupUsers.Find(id);
            if (aspNetGroupUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetGroupUser);
        }

        // POST: AspNetGroupUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,UserId")] AspNetGroupUser aspNetGroupUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetGroupUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetGroupUser);
        }

        // GET: AspNetGroupUsers/Delete/5
        public ActionResult Delete(int? id, string username)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            string uid;
            string queryString = "SELECT dbo.AspNetUsers.Id "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE dbo.AspNetUsers.UserName='" + username + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                uid = reader.GetString(0);
                reader.Close();
            }
            AspNetGroupUser aspNetGroupUser = db.AspNetGroupUsers.Find(id, uid);
            if (aspNetGroupUser == null)
            {
                return HttpNotFound("The id is " + id + " and the uid is " + uid);
            }
            return View(aspNetGroupUser);
        }

        // POST: AspNetGroupUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int groupId, string username)
        {
            string userId;
            string queryString = "SELECT dbo.AspNetUsers.Id "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE dbo.AspNetUsers.UserName='" + username + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                userId = reader.GetString(0);
                reader.Close();
            }
            AspNetGroupUser aspNetGroupUser = db.AspNetGroupUsers.Find(groupId, userId);
            db.AspNetGroupUsers.Remove(aspNetGroupUser);
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
