using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspDotNetProject.Models;


namespace AspDotNetProject.Controllers
{
    public class LoginController : Controller
    {
        private DataFristEntities db = new DataFristEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.user1.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user1 user1 = db.user1.Find(id);
            if (user1 == null)
            {
                return HttpNotFound();
            }
            return View(user1);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Email,Password")] user1 user1)
        {
            if (ModelState.IsValid)
            {
                db.user1.Add(user1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user1);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user1 user1 = db.user1.Find(id);
            if (user1 == null)
            {
                return HttpNotFound();
            }
            return View(user1);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Email,Password")] user1 user1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user1);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user1 user1 = db.user1.Find(id);
            if (user1 == null)
            {
                return HttpNotFound();
            }
            return View(user1);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user1 user1 = db.user1.Find(id);
            db.user1.Remove(user1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Index1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index1(user1 s)
        {
            if(ModelState.IsValid==true)
            {
                var data = db.user1.Where(model => model.Name == s.Name && model.Password == s.Password).FirstOrDefault();
                if(data==null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    return View();
                }
                else
                {
                    Session["Name"] = s.Name;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult Logout()

        {
            Session.Abandon();
            return RedirectToAction("Index1","Login");
        }
        public ActionResult Reset()
        {
            ModelState.Clear();
            return RedirectToAction("Index1","Login");
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
