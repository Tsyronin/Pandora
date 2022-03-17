using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandoraMVC.Entities
{
    public class Workspace
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public virtual ICollection<Epic> Epics { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
