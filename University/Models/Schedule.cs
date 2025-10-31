using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Models;

public class Schedule
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; }
    public string Time { get; set; }
    public string Subject { get; set; }
    public string Teacher { get; set; }
    public string Classroom { get; set; }
    public string Group { get; set; }
}

// Models/ScheduleViewModel.cs
public class ScheduleViewModel
{
    public List<Schedule> Schedules { get; set; }
    public string SelectedGroup { get; set; }
    public List<string> Groups { get; set; }
}