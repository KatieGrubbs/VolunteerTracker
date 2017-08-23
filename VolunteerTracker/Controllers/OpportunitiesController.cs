using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VolunteerTracker.Models;
using VolunteerTracker.Models.Opportunity;

namespace VolunteerTracker.Controllers
{
    [Authorize]
    public class OpportunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Opportunities
        public ActionResult Index()
        {
            return View(db.Opportunities.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchString, string filter)
        {
            var opportunities = from o in db.Opportunities select o;

            // If no search criteria is given or if no filters are set
            //if (String.IsNullOrEmpty(searchString) && filter.IsNullOrWhiteSpace())
            //{
            //    return View(opportunities.ToList());
            //}

            // Search criteria is given
            if (!String.IsNullOrEmpty(searchString))
            {
                opportunities = opportunities.Where(o => o.OpportunityName.Contains(searchString));
            }

            // A new filter is set
            switch (filter)
            {
                case "By Center Ascending":
                    opportunities = opportunities.OrderBy(o => o.EventCenter);
                    break;
                case "By Center Descending":
                    opportunities = opportunities.OrderByDescending(o => o.EventCenter);
                    break;
                case "Most Recent":
                    DateTime dateMax = DateTime.Now.AddDays(60);
                    // Filter by opportunities in the last 60 days
                    opportunities = opportunities.Where(o => o.OpportunityDate < dateMax);
                    // Sort by most recent
                    opportunities = opportunities.OrderByDescending(o => o.OpportunityDate);
                    break;
            }

            return View(opportunities.ToList());
        }

        public ActionResult VolunteerMatches(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Opportunity opportunity = db.Opportunities.Find(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }

            string opportunityName = opportunity.OpportunityName;
            ViewBag.opportunityName = opportunityName;

            var volunteerMatches = from v in db.Volunteers
                where v.AvailableDay.ToString() == opportunity.EventDay.ToString()
                select v;

            return View(volunteerMatches.ToList());
        }

        // GET: Opportunities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Opportunity opportunity = db.Opportunities.Find(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }

            return View(opportunity);
        }

        // GET: Opportunities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opportunities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpportunityId,OpportunityName,OpportunityDate,EventDay,EventCenter")] Opportunity opportunity)
        {
            if (ModelState.IsValid)
            {
                db.Opportunities.Add(opportunity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(opportunity);
        }

        // GET: Opportunities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Opportunity opportunity = db.Opportunities.Find(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }

            return View(opportunity);
        }

        // POST: Opportunities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpportunityId,OpportunityName,OpportunityDate,EventDay,EventCenter")] Opportunity opportunity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opportunity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(opportunity);
        }

        // GET: Opportunities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Opportunity opportunity = db.Opportunities.Find(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }

            return View(opportunity);
        }

        // POST: Opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opportunity opportunity = db.Opportunities.Find(id);
            db.Opportunities.Remove(opportunity);
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
