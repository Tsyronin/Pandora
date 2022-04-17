using PandoraMVC.Data;
using PandoraMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace PandoraMVC.Services
{
    public class EpicService
    {
        private readonly ApplicationDbContext _context;

        public EpicService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IEnumerable<GanttDataSource> GetGanttDataSources()
        {
            IEnumerable<Epic> epics = GetEpicsWithTasks(); //TODO: add filtering by workspace

            IEnumerable<GanttDataSource> ganttSources = ConvertEpicsToGanttDataSources(epics);

            return ganttSources;
        }

        private IEnumerable<Epic> GetEpicsWithTasks() => _context.Epics.Include(e => e.Tasks).ToList();

        private IEnumerable<GanttDataSource> ConvertEpicsToGanttDataSources(IEnumerable<Epic> epics) 
            => epics.Select(e => ConvertEpicToGanttDataSource(e));

        private GanttDataSource ConvertEpicToGanttDataSource(Epic epic)
        {
            var tasks = _context.Tasks.Where(t => t.EpicId == epic.Id);

            var epicSource = new GanttDataSource()
            {
                taskId = epic.Id,
                taskName = epic.Name,
                isEpic = true
            };

            var convertedTasks = tasks.Select(t => new GanttDataSource() 
            {
                taskId = t.Id,
                taskName = t.Title,
                startDate = t.StartDate,
                endDate = t.EndDate,
                notes = t.Description,
                progress = t.Status == true? 100 : 0,
                isEpic = false
            });

            epicSource.subTasks = convertedTasks.ToList();

            return epicSource;
        }
    
        public Epic AddEpic(Epic epic)
        {
            epic.Id = 0;
            _context.Epics.Add(epic);
            _context.SaveChanges();
            return epic;
        }

        public Epic GetEpicById(int epicId)
        {
            var epic = _context.Epics.FirstOrDefault(e => e.Id == epicId);
            return epic;
        }

        public void EditEpic(Epic epic)
        {
            _context.Entry(epic).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEpicById(int epicId)
        {
            var epic = _context.Epics.FirstOrDefault(e => e.Id == epicId);
            _context.Epics.Remove(epic);
            _context.SaveChanges();
        }
    }
}
