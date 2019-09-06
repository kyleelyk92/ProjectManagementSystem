using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem2.Models
{
    public class ProjTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public bool CompletionStatus { get; set; }
        public enum Priority{ Low, Med, High}
        public string Notes { get; set; }
        public int CompletionPercentage { get; set; }
        public virtual ICollection<ApplicationUser> Developers { get; set; }
        public ProjTask()
        {
            Developers = new HashSet<ApplicationUser>();
            CompletionPercentage = 0;
        }
    }
}