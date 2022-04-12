using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PandoraMVC.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PandoraMVC.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<GanttDataSource> GetData()
        {
            return new List<GanttDataSource>()
            {
                new GanttDataSource()
                {
                    taskId=1,
                    taskName="sds",
                    startDate=new DateTime(2022, 2, 1),
                    endDate=new DateTime(2022, 2, 5),
                    duration=4,
                    progress=0,
                    subTasks=new List<GanttDataSource>(){},
                    isEpic=false
                },
                new GanttDataSource()
                {
                    taskId=2,
                    taskName="ssadsd",
                    startDate=new DateTime(2022, 2, 5),
                    endDate=new DateTime(2022, 2, 10),
                    duration=6,
                    progress=10,
                    subTasks=new List<GanttDataSource>(){},
                    isEpic=false
                }
            };
        }

        public ActionResult UrlDatasource(DataManagerRequest dm)
        {
            return Json(new { result = GetData(), count = GetData().Count() }) ;
        }

        public class ICRUDModel<T> where T : class
        {

            public object key { get; set; }

            public T value { get; set; }

            public List<T> added { get; set; }

            public List<T> changed { get; set; }

            public List<T> deleted { get; set; }

        }
        public ActionResult BatchSave([FromBody] ICRUDModel<GanttDataSource> data)
        {

            List<GanttDataSource> uAdded = new List<GanttDataSource>();
            List<GanttDataSource> uChanged = new List<GanttDataSource>();
            List<GanttDataSource> uDeleted = new List<GanttDataSource>();

            if (data.added != null && data.added.Count() > 0)
            {
                foreach (var rec in data.added)
                {
                    uAdded.Add(this.Create(rec));
                }
            }

            //if (data.changed != null && data.changed.Count() > 0)
            //{
            //    foreach (var rec in data.changed)
            //    {
            //        uChanged.Add(this.Edit(rec));
            //    }
            //}

            //if (data.deleted != null && data.deleted.Count() > 0)
            //{
            //    foreach (var rec in data.deleted)
            //    {
            //        uDeleted.Add(this.Delete(rec.taskId));
            //    }
            //}

            return Json(new { addedRecords = uAdded, changedRecords = uChanged, deletedRecords = uDeleted });
        }

        public GanttDataSource Create(GanttDataSource value)
        {
            return value;
        }

        public GanttDataSource Edit(GanttDataSource value)
        {
            //GanttData result = db.GanttDatas.Where(currentData => currentData.TaskId == value.TaskId).FirstOrDefault();
            //if (result != null)
            //{
            //    result.TaskId = value.TaskId;
            //    result.TaskName = value.TaskName;
            //    result.StartDate = value.StartDate;
            //    result.EndDate = value.EndDate;
            //    result.Duration = value.Duration;
            //    result.Progress = value.Progress;
            //    return result;
            //}
            //else
            //{
                return null;
            
        }

        public GanttDataSource Delete(int value)
        {
            //var result = db.GanttDatas.Where(currentData => currentData.TaskId == value).FirstOrDefault();
            //db.GanttDatas.Remove(result);
            //RemoveChildRecords(value);
            //db.SaveChanges();
            return null;
        }
        public void RemoveChildRecords(string key)
        {
            //var childList = db.GanttDatas.Where(x => x.ParentId == key).ToList();
            //foreach (var item in childList)
            //{
            //    db.GanttDatas.Remove(item);
            //    RemoveChildRecords(item.TaskId);
            //}
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