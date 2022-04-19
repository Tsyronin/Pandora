using System;
using System.Collections.Generic;

public class GanttDataSource
{
    public int taskId { get; set; } 
    public string taskName { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public double? duration { get; set; }
    public int progress { get; set; }
    public List<GanttDataSource> subTasks { get; set; }
    public bool isEpic { get; set; }
    public string notes { get; set; }
}