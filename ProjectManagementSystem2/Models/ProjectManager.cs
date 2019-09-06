using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem2.Models
{
    public class ProjectManager
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void AddNewProject(string projName)
        {

            if(db.Projects.Find(projName) != null)
            {
                return;
            }
            else
            {
                db.Projects.Add(new Project() { ProjectName = projName });
            }
            db.SaveChanges();
        }
        public void RemoveProject(string projName)
        {
            var proj = db.Projects.Find(projName);

            if (proj != null)
            {
                db.Projects.Remove(proj);
            }
            db.SaveChanges();
        }
        public void UpdateProject(string projName)
        {
            var project = db.Projects.Find(projName);

            if(project.CompletionStatus == false)
            {
                project.CompletionStatus = true;
            }
            db.SaveChanges();
        }
    }
}