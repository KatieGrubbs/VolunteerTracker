using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using VolunteerTracker.Models;
using VolunteerTracker.Models.Volunteer;
using VolunteerTracker.Models.Opportunity;
using VolunteerTracker.Models.Volunteer;

namespace VolunteerTracker.Controllers
{
    [Authorize]
    public class VolunteersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Volunteers
        public ActionResult Index()
        {
            return View(db.Volunteers.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchString, string filter)
        {
            var volunteers = from v in db.Volunteers select v;

            // If no search criteria is given and if no filters are set
            if (String.IsNullOrEmpty(searchString) && filter.Equals("All"))
            {
                return View(volunteers.ToList());
            }

            // If search criteria is given
            if (!String.IsNullOrEmpty(searchString))
            {
                volunteers = volunteers.Where(v => (v.FirstName + " " + v.LastName).Contains(searchString));
            }

            // A new filter is set
            switch (filter)
            {
                case "Approved":
                    volunteers = volunteers.Where(v => v.Status.ToString().Equals("Approved"));
                    break;
                case "Pending":
                    volunteers = volunteers.Where(v => v.Status.ToString().Equals("Pending"));
                    break;
                case "Approved/Pending":
                    volunteers = volunteers.Where(v => v.Status.ToString().Equals("Approved") || v.Status.ToString().Equals("Pending"));
                    break;
                case "Disapproved":
                    volunteers = volunteers.Where(v => v.Status.ToString().Equals("Disapproved"));
                    break;
                case "Inactive":
                    volunteers = volunteers.Where(v => v.Status.ToString().Equals("Inactive"));
                    break;
            }

            return View(volunteers.ToList());
        }

        public ActionResult ViewOpportunities(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Volunteer volunteer = db.Volunteers.Find(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }

            string fullName = volunteer.FirstName + " " + volunteer.LastName;
            ViewBag.volunteerName = fullName;

            var eventMatches = from o in db.Opportunities
                               where volunteer.AvailableDay.ToString() == o.EventDay.ToString()
                               select o;

            return View(eventMatches.ToList());
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Volunteer volunteer = db.Volunteers.Find(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolunteerId,FirstName,LastName,AvailableDay,CenterPreferred,HighestEducation,Skill,CurrentLicense,UserName,Password,Address,HomeNumber,CellNumber,WorkNumber,Email,EmergencyContact,HasDriversLicense,HasSsCard,Status")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Volunteers.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Volunteer volunteer = db.Volunteers.Find(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolunteerId,FirstName,LastName,AvailableDay,CenterPreferred,HighestEducation,Skill,CurrentLicense,UserName,Password,Address,HomeNumber,CellNumber,WorkNumber,Email,EmergencyContact,HasDriversLicense,HasSsCard,Status")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Volunteer volunteer = db.Volunteers.Find(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            db.Volunteers.Remove(volunteer);
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
