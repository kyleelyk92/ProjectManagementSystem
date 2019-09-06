using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagementSystem2.Models;
using Tulpep.NotificationWindow;

namespace ProjectManagementSystem2.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
        [Authorize(Roles="ProjectManager")]
        public ActionResult ManagerViewProject(int? projectId, bool orderByPriority)
        {
            var project = db.Projects.Find(projectId);

            if(orderByPriority == false)
            {
                var taskList = db.Tasks.Where(t => t.ProjectId == projectId).OrderByDescending(t => t.CompletionPercentage).Include(t=> t.Developers).ToList();
                ViewBag.Tasks = taskList;
            }
            //else
            //{
            //    var taskList = db.Tasks.Where(t => t.ProjectId == projectId).OrderByDescending(t => Enum.Parse(t.Priority, ).Include(t => t.Developers).ToList();
            //}
            //couldn't figure this out right away, needs more work
            

            return View(project);
        }

        [Authorize(Roles = "ProjectManager")]
        public ActionResult ManagerDashboard()
        {
            var listOfProjects = db.Projects.Include(p => p.Tasks).ToList();


            //manager notification if something is overdue
            //if(db.Projects.Any(p => DateTime.Now > p.Deadline))
            //{
            //MyHub1.Hello();
            //}

            return View(listOfProjects);
        }
        [Authorize(Roles = "ProjectManager")]
        public ActionResult PastDueDateNotComplete()
        {
            var listOfProjects = db.Projects.Where(p => DateTime.Now > p.Deadline && p.CompletionStatus != true);

            return View(listOfProjects);
        }

    }
}
