using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagementSystem2.Models;
using Microsoft.AspNet.Identity;

namespace ProjectManagementSystem2.Controllers
{
    public class ProjTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjTasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(p => p.Project);
            return View(tasks.ToList());
        }

        // GET: ProjTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjTask projTask = db.Tasks.Find(id);
            if (projTask == null)
            {
                return HttpNotFound();
            }
            return View(projTask);
        }

        // GET: ProjTasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName");
            return View();
        }

        // POST: ProjTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TaskName,ProjectId,CompletionStatus,Notes,CompletionPercentage")] ProjTask projTask)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(projTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", projTask.ProjectId);
            return View(projTask);
        }

        // GET: ProjTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }   
            ProjTask projTask = db.Tasks.Find(id);
            if (projTask == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", projTask.ProjectId);
            return View(projTask);
        }

        // POST: ProjTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TaskName,ProjectId,CompletionStatus,Notes,CompletionPercentage")] ProjTask projTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", projTask.ProjectId);
            return View(projTask);
        }

        // GET: ProjTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjTask projTask = db.Tasks.Find(id);
            if (projTask == null)
            {
                return HttpNotFound();
            }
            return View(projTask);
        }

        // POST: ProjTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjTask projTask = db.Tasks.Find(id);
            db.Tasks.Remove(projTask);
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
