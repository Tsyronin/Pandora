using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandoraMVC.Entities
{
    public class User : IdentityUser
    {
        public int WorkspaceId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Picture { get; set; }


        public virtual Workspace Workspace { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

    }
}
