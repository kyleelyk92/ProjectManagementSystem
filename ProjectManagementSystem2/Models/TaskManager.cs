using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectManagementSystem2.Models
{
    [Authorize(Roles="ProjectManager")]
    public class TaskManager
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void AddTaskToProject(string taskName, string projectName)
        {
            var proj = db.Projects.FirstOrDefault(p => p.ProjectName == projectName);

            if(proj != null)
            {
                var task = new ProjTask();
                task.TaskName = taskName;
                proj.Tasks.Add(task);
            }
            db.SaveChanges();
        }

        public void RemoveTaskFromProject(string taskName, string projectName)
        {
            var proj = db.Projects.FirstOrDefault(p => p.ProjectName == projectName);
            
            if (proj != null)
            {
                var task = proj.Tasks.FirstOrDefault(t => t.TaskName == taskName);
                proj.Tasks.Remove(task);
                db.SaveChanges();
            }
        }

        public void UpdateTask(string taskId)
        {
            var task = db.Tasks.Find(taskId);

            if(task != null)
            {
                if(task.CompletionStatus == false)
                {
                    task.CompletionStatus = true;
                }
                else
                {
                    task.CompletionStatus = false;
                }
            }
            db.SaveChanges();
        }

        public async System.Threading.Tasks.Task AssignDeveloperToTaskAsync(string taskName, string developerName)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>());

            var task = db.Tasks.FirstOrDefault(t => t.TaskName == taskName);
            var developer = db.Users.FirstOrDefault(u => u.UserName == developerName);

            var role = db.Roles.FirstOrDefault(r => r.Name == "developer");

            bool roleCheck = await manager.IsInRoleAsync(developer.Id, "Developer");
            
            if(roleCheck == true)
            {
                task.Developers.Add(developer);
                db.SaveChanges();
            }
        }
    }
}