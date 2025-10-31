// Controllers/ScheduleController.cs
using Microsoft.AspNetCore.Mvc;

public class ScheduleController : Controller
{
    private List<Schedule> GetSampleSchedules()
    {
        return new List<Schedule>
        {
            new Schedule { Id = 1, DayOfWeek = "Понедельник", Time = "9:00-10:30", Subject = "Математика", Teacher = "Иванов А.П.", Classroom = "101", Group = "ИТ-21" },
            new Schedule { Id = 2, DayOfWeek = "Понедельник", Time = "10:45-12:15", Subject = "Программирование", Teacher = "Петрова С.М.", Classroom = "203", Group = "ИТ-21" },
            new Schedule { Id = 3, DayOfWeek = "Понедельник", Time = "13:00-14:30", Subject = "Физика", Teacher = "Сидоров В.К.", Classroom = "105", Group = "ИТ-21" },

            new Schedule { Id = 4, DayOfWeek = "Вторник", Time = "9:00-10:30", Subject = "Базы данных", Teacher = "Кузнецова О.Л.", Classroom = "301", Group = "ИТ-21" },
            new Schedule { Id = 5, DayOfWeek = "Вторник", Time = "10:45-12:15", Subject = "Веб-разработка", Teacher = "Николаев Д.С.", Classroom = "205", Group = "ИТ-21" },

            new Schedule { Id = 6, DayOfWeek = "Среда", Time = "9:00-10:30", Subject = "Иностранный язык", Teacher = "Смирнова Е.В.", Classroom = "402", Group = "ИТ-21" },
            new Schedule { Id = 7, DayOfWeek = "Среда", Time = "10:45-12:15", Subject = "Алгоритмы", Teacher = "Федоров П.Н.", Classroom = "210", Group = "ИТ-21" },

            new Schedule { Id = 8, DayOfWeek = "Четверг", Time = "9:00-10:30", Subject = "Сети", Teacher = "Васильев М.К.", Classroom = "305", Group = "ИТ-21" },
            new Schedule { Id = 9, DayOfWeek = "Четверг", Time = "10:45-12:15", Subject = "Математика", Teacher = "Иванов А.П.", Classroom = "101", Group = "ИТ-21" },

            new Schedule { Id = 10, DayOfWeek = "Пятница", Time = "9:00-10:30", Subject = "Физкультура", Teacher = "Тихонов С.П.", Classroom = "Спортзал", Group = "ИТ-21" },
            new Schedule { Id = 11, DayOfWeek = "Пятница", Time = "10:45-12:15", Subject = "Проектная деятельность", Teacher = "Петрова С.М.", Classroom = "203", Group = "ИТ-21" }
        };
    }

    public IActionResult Index(string group = "ИТ-21")
    {
        var schedules = GetSampleSchedules();
        var groups = new List<string> { "ИТ-21", "ИТ-22", "ЭК-21", "ЮР-21", "ФЛ-21" };

        var viewModel = new ScheduleViewModel
        {
            Schedules = schedules.Where(s => s.Group == group).ToList(),
            SelectedGroup = group,
            Groups = groups
        };

        return View("~/Views/Home/schedules.cshtml", viewModel);
    }
}