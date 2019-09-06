using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<ProjTask> Tasks { get; set; }
        public bool CompletionStatus { get; set; }
        public DateTime Deadline { get; set; }
        public Project()
        {
            Tasks = new HashSet<ProjTask>();
        }
    }
}