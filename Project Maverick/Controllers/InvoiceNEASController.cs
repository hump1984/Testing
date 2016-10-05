using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using Microsoft.AspNet.Identity;
using Project_Maverick.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_Maverick.Controllers
{
    public class InvoiceNEASController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        static InvoiceModelNEAS currentInvoiceModel;

        // GET: InvoiceNEAS
        public ActionResult Index()
        {
            return View(db.InvoiceNEAS.ToList());
        }

        // GET: InvoiceNEAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceModelNEAS invoiceModelNEAS = db.InvoiceNEAS.Find(id);

            if (invoiceModelNEAS == null)
            {
                return HttpNotFound();
            }

            currentInvoiceModel = invoiceModelNEAS;

            return View(invoiceModelNEAS);
        }

        // GET: InvoiceNEAS/Create
        public ActionResult Create()
        {
            ViewBag.Projects = GetProjects();

            return View();
        }

        // POST: InvoiceNEAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Bind(Include = "ID,DateCreated,InvoiceNumber,Controlled,ControlDate,Approved,ApproveDate,BelongsToProject")]
        public ActionResult Create(InvoiceModelNEAS invoiceModelNEAS, string selectedProject)
        {
            ViewBag.Projects = GetProjects();

            if (ModelState.IsValid)
            {
                invoiceModelNEAS.ProjectID = System.Convert.ToInt32(selectedProject);
                
                db.InvoiceNEAS.Add(invoiceModelNEAS);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(invoiceModelNEAS);
        }

        public ActionResult _ListPosts()
        {
            List<InvoicePosts> posts = new List<InvoicePosts>();
            posts = db.InvoicePosts.Where(u => u.InvoiceID.Equals(currentInvoiceModel.ID)).ToList();

            decimal totalSum = 0;

            foreach (var item in posts)
            {
                item.Total = item.ContractPostModels.Price * item.Amount;

                totalSum += item.Total;
            }

            ViewBag.ItemsTotal = totalSum.ToString();

            return View(posts);
        }

        
        public ActionResult _AddPost(int postID)
        {
            ViewBag.ContractPosts = GetContractPosts();

            var newPost = new InvoicePosts();

            InvoiceModelNEAS invoiceModel = db.InvoiceNEAS.Find(postID);//Change names here
            
            newPost.InvoiceID = invoiceModel.ID; // this will be sent from the ArticleDetails View, hold on :).
            
            return View();

            //return RedirectToAction("Details", new { id = newPost.InvoiceID });
        }

        [HttpPost]
        public ActionResult _AddPost(InvoicePosts contractPost, string selectedContractPost)
        {
            ViewBag.ContractPosts = GetContractPosts();

            if (ModelState.IsValid)
            {
                //var curUser = GetCurrentUser();
                //hour.user = curUser;

                contractPost.ContractPostID = System.Convert.ToInt32(selectedContractPost);
                contractPost.InvoiceID = currentInvoiceModel.ID;

                db.InvoicePosts.Add(contractPost);

                db.SaveChanges();

                return View();
            }

            return View();

        }

        // GET: InvoicePosts/Delete/5
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoicePosts post = db.InvoicePosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: InvoicePosts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            InvoicePosts post = db.InvoicePosts.Find(id);
            db.InvoicePosts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = post.InvoiceID });
        }






        public List<SelectListItem> GetProjects()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.Projects)
            {
                items.Add(new SelectListItem { Text = item.PID.ToString() + " - " + item.Name, Value = item.Id.ToString() });
            }

            return items;
        }

        // GET: InvoiceNEAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceModelNEAS invoiceModelNEAS = db.InvoiceNEAS.Find(id);
            if (invoiceModelNEAS == null)
            {
                return HttpNotFound();
            }
            return View(invoiceModelNEAS);
        }

        // POST: InvoiceNEAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateCreated,InvoiceNumber,Controlled,ControlDate,Approved,ApproveDate")] InvoiceModelNEAS invoiceModelNEAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceModelNEAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceModelNEAS);
        }

        // GET: InvoiceNEAS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceModelNEAS invoiceModelNEAS = db.InvoiceNEAS.Find(id);
            if (invoiceModelNEAS == null)
            {
                return HttpNotFound();
            }
            return View(invoiceModelNEAS);
        }

        // POST: InvoiceNEAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceModelNEAS invoiceModelNEAS = db.InvoiceNEAS.Find(id);
            db.InvoiceNEAS.Remove(invoiceModelNEAS);
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

        public List<SelectListItem> GetContractPosts()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.ContractPosts)
            {
                items.Add(new SelectListItem { Text = item.PostCode.ToString() + " - " + item.Desc, Value = item.ID.ToString() });
            }

            return items;
        }
    }
}
