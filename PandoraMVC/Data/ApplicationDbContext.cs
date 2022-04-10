using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PandoraMVC.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Configuration;

namespace PandoraMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
    }
}
