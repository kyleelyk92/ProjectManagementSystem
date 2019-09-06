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
    public class UsersManager
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void AddANewUser(string userName, string password)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>());

            var user = new ApplicationUser();
            user.Email = userName;
            user.UserName = userName;

            manager.CreateAsync(user, password);

            db.SaveChanges();
        }
        public void RemoveUser(string userName)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>());
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);

            manager.DeleteAsync(user);
            db.SaveChanges();
        }
        public void AddUserToRole(string userName, string roleName)
        {
            if (Roles.RoleExists(roleName))
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>());
                var role = db.Roles.FirstOrDefault(r => r.Name == roleName);
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);

                manager.AddToRoleAsync(user.Id, role.Name);

                db.SaveChanges();
            }
        }

        public void RemoveUserFromRole(string userName, string roleName)
        {
            if (Roles.RoleExists(roleName))
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>());
                var role = db.Roles.FirstOrDefault(r => r.Name == roleName);
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);

                manager.RemoveFromRoleAsync(user.Id, role.Name);
                db.SaveChanges();
            }
        }
    }
}