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

    public class HoursController : Controller
    {

        protected UserManager<ApplicationUser> UserManager { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        private HourViewModels viewModels = new HourViewModels();

        public HoursController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }
        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>

        // GET: Hours
        public ActionResult Index()
        {
            //ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var user = GetCurrentUser();
            var hours = db.Hours.Where(u => u.user.Id.Equals(user.Id));

            return View(hours);
        }

        // GET: Hours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hour hour = db.Hours.Find(id);
            if (hour == null)
            {
                return HttpNotFound();
            }
            return View(hour);
        }

        // GET: Hours/Create
        public ActionResult Create()
        {

            ViewBag.Projects = GetProjects();

            return View();
        }

        // POST: Hours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegDateTime,StartDateTime,StopDateTime,Amount,Comment,RegType")] Hour hour)
        {
            if (ModelState.IsValid)
            {
                var curUser = GetCurrentUser();

                hour.user = curUser;
                db.Hours.Add(hour);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hour);
        }

        // GET: Hours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hour hour = db.Hours.Find(id);
            if (hour == null)
            {
                return HttpNotFound();
            }
            return View(hour);
        }

        // POST: Hours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegDateTime,StartDateTime,StopDateTime,Amount,Comment,RegType")] Hour hour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hour);
        }

        // GET: Hours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hour hour = db.Hours.Find(id);
            if (hour == null)
            {
                return HttpNotFound();
            }
            return View(hour);
        }

        // POST: Hours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hour hour = db.Hours.Find(id);
            db.Hours.Remove(hour);
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

        public ActionResult RegisterHours(int? userID, int? projectID, Hour newHour, string atDate)
        {
            //Select Project List

            ViewBag.Projects = GetProjects();
            //Select Project List

            if (atDate != null)
            {
                DateTime datetime = DateTime.ParseExact(atDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                newHour.RegDateTime = datetime;
                ViewBag.atDate = datetime; //??
            }

            return View();
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

        [HttpPost]
        public ActionResult RegisterHours(Hour newHour,string selectedProject)
        {
            if (ModelState.IsValid)
            {
                var curUser = GetCurrentUser();

                newHour.user = curUser;
                newHour.project = db.Projects.Find(System.Convert.ToInt32(selectedProject));

                db.Hours.Add(newHour);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(newHour);
        }

        [HttpPost]
        public ActionResult RegisterHourAtDate()
        {
            return View();
        }

        //public DateTime currentViewDate;
        //public int currentWeek = -1;

        public ActionResult Overview()
        {
            //if (currentWeek == -1)
            //{
             //   currentWeek = viewModels.GetCurrentWeek(DateTime.Today);
            //}
            var curUser = GetCurrentUser();
            var hours = db.Hours.Where(u => u.user.Id.Equals(curUser.Id));
            var totHours = 0;

            foreach (var item in hours)
            {
                totHours += item.Amount;
            }

            ViewBag.TotalHours = totHours;

            ViewBag.CurrentWeek = viewModels.currentWeek;
            ViewBag.CurrentDate = viewModels.currentViewDate;
            ViewBag.DatesAndHours = viewModels.datesAndHours;

            //setWeek(0);

            return View();
        }

        [HttpPost]
        public ActionResult Overview(int changeWeek)
        {
            viewModels.UpdateDates(changeWeek);
            var curUser = GetCurrentUser();
            var hours = db.Hours.Where(u => u.user.Id.Equals(curUser.Id));
            var totHours = 0;

            foreach (var item in hours)
            {
                totHours += item.Amount;
            }

            ViewBag.TotalHours = totHours;

            ViewBag.CurrentWeek = viewModels.currentWeek;
            ViewBag.CurrentDate = viewModels.currentViewDate;
            ViewBag.DatesAndHours = viewModels.datesAndHours;

            return View();
        }

        public ApplicationUser GetCurrentUser()
        {
            //Gets Current user
            var curUser = UserManager.FindById(User.Identity.GetUserId());
            return (curUser);
        }
    }
}
