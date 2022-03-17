using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandoraMVC.Entities
{
    public class Epic
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }

        public string Name { get; set; }


        public virtual Workspace Workspace { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
