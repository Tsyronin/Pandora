using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PandoraMVC.Entities;
using PandoraMVC.Models;
using PandoraMVC.Services;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PandoraMVC.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EpicService _epicService;
        private readonly TaskService _taskService;
        private readonly ActionRecognizer _actionRecognizer;

        public HomeController(ILogger<HomeController> logger, EpicService epicService, TaskService taskService, ActionRecognizer actionRecognizer)
        {
            _logger = logger;
            _epicService = epicService;
            _taskService = taskService;
            _actionRecognizer = actionRecognizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult UrlDatasource(DataManagerRequest dm)
        {
            var data = GetData();

            return Json(new { result = data, count = data.Count() });
        }

        private List<GanttDataSource> GetData()
        {
            var ganttDataSources = _epicService.GetGanttDataSources();

            return ganttDataSources.ToList();
        }

        public ActionResult BatchSave([FromBody] ICRUDModel<GanttDataSource> data)
        {
            List<GanttDataSource> uAdded = new List<GanttDataSource>();
            List<GanttDataSource> uChanged = new List<GanttDataSource>();
            List<GanttDataSource> uDeleted = new List<GanttDataSource>();

            ProcessAction(data, uAdded, uChanged, uDeleted);

            return Json(new { addedRecords = uAdded, changedRecords = uChanged, deletedRecords = uDeleted });
        }

        private void ProcessAction(ICRUDModel<GanttDataSource> data, List<GanttDataSource> uAdded, List<GanttDataSource> uChanged, List<GanttDataSource> uDeleted)
        {
            switch (_actionRecognizer.RecognizeAction(data))
            {
                case Helpers.UiAction.AddEpic:
                    AddEpic(data.added.First(), uAdded);
                    break;
                case Helpers.UiAction.AddTask:
                    AddTask(data.added.First(), data.changed.First(), uAdded);
                    break;
                case Helpers.UiAction.EditTask:
                    EditTask(data.changed.First(), uChanged);
                    break;
                case Helpers.UiAction.EditEpic:
                    EditEpic(data.changed.First(), uChanged);
                    break;
                case Helpers.UiAction.DeleteTask:
                    DeleteTask(data.deleted.First(), uDeleted);
                    break;
                case Helpers.UiAction.DeleteEpic:
                    DeleteEpic(data.deleted.First(), uDeleted);
                    break;
                case Helpers.UiAction.Unknown:
                    break;
            }
        }

        private void AddEpic(GanttDataSource epicGDS, List<GanttDataSource> uAdded)
        {
            var epic = new Epic() 
            {
                WorkspaceId = 2, //TODO: assign workspace of current user
                Name = epicGDS.taskName
            };
            _epicService.AddEpic(epic);
            epicGDS.taskId = epic.Id;
            epicGDS.isEpic = true;

            uAdded.Add(epicGDS);
        }

        private void AddTask(GanttDataSource taskGDS, GanttDataSource epicGDS, List<GanttDataSource> uAdded)
        {
            taskGDS.startDate = DateTime.Now;
            taskGDS.endDate = DateTime.Now;

            var task = new Task()
            {
                Id = 0,
                Title = taskGDS.taskName,
                Description = taskGDS.notes,
                StartDate = taskGDS.startDate,
                EndDate = taskGDS.endDate,
                Status = false,
                EpicId = epicGDS.taskId
            };
            _taskService.AddTask(task);

            taskGDS.taskId = task.Id;
            taskGDS.isEpic = false;


            uAdded.Add(taskGDS);
        }

        private void EditTask(GanttDataSource taskGDS, List<GanttDataSource> uChanged)
        {
            var task = _taskService.GetTaskById(taskGDS.taskId);
            if (task == null)
                return;

            task.Title = taskGDS.taskName;
            task.Description = taskGDS.notes;
            task.StartDate = taskGDS.startDate;
            task.EndDate = taskGDS.endDate;
            task.Status = taskGDS.progress > 0 ? true : false;

            _taskService.UpdateTask(task);

            uChanged.Add(taskGDS);
        }

        private void EditEpic(GanttDataSource epicGDS, List<GanttDataSource> uChanged)
        {
            var epic = _epicService.GetEpicById(epicGDS.taskId);
            if (epic == null)
                return;

            epic.Name = epicGDS.taskName;

            _epicService.EditEpic(epic);

            uChanged.Add(epicGDS);
        }

        private void DeleteTask(GanttDataSource taskGDS, List<GanttDataSource> uDeleted)
        {
            _taskService.DeleteTaskById(taskGDS.taskId);
            uDeleted.Add(taskGDS);
        }

        private void DeleteEpic(GanttDataSource epicGDS, List<GanttDataSource> uDeleted)
        {
            _epicService.DeleteEpicById(epicGDS.taskId);
            uDeleted.Add(epicGDS);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}