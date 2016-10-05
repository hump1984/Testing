using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Maverick.Models;

namespace Project_Maverick.Controllers
{
    public class ContractPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractPost
        public ActionResult Index()
        {
            return View(db.ContractPosts.ToList());
        }

        // GET: ContractPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractPostModel contractPostModel = db.ContractPosts.Find(id);
            if (contractPostModel == null)
            {
                return HttpNotFound();
            }
            return View(contractPostModel);
        }

        // GET: ContractPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostCode,Desc,Price,Unit")] ContractPostModel contractPostModel)
        {
            if (ModelState.IsValid)
            {
                db.ContractPosts.Add(contractPostModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractPostModel);
        }

        // GET: ContractPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractPostModel contractPostModel = db.ContractPosts.Find(id);
            if (contractPostModel == null)
            {
                return HttpNotFound();
            }
            return View(contractPostModel);
        }

        // POST: ContractPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostCode,Desc,Price,Unit")] ContractPostModel contractPostModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractPostModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractPostModel);
        }

        // GET: ContractPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractPostModel contractPostModel = db.ContractPosts.Find(id);
            if (contractPostModel == null)
            {
                return HttpNotFound();
            }
            return View(contractPostModel);
        }

        // POST: ContractPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractPostModel contractPostModel = db.ContractPosts.Find(id);
            db.ContractPosts.Remove(contractPostModel);
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
