using Microsoft.EntityFrameworkCore;
using PandoraMVC.Data;
using PandoraMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PandoraMVC.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public Task AddTask(Task task)
        {
            task.Id = 0;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public Task GetTaskById(int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            return task;
        }

        public void UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTaskById(int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
