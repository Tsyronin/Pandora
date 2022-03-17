using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PandoraMVC.Entities
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public string AssigneeId { get; set; }

        public bool Status { get; set; }

        public int EpicId { get; set; }


        public virtual Epic Epic { get; set; }

        public virtual User Workspace { get; set; }
    }
}
