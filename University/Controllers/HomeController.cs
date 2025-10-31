using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityInformationSystem.Data;

namespace UniversityInformationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UniversityContext _context;

        public HomeController(UniversityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var statistics = new
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalTeachers = await _context.Teachers.CountAsync(),
                TotalFaculties = await _context.Faculties.CountAsync(),
                TotalCourses = await _context.Courses.CountAsync()
            };

            ViewBag.Statistics = statistics;
            return View();
        }

        public IActionResult Students()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Teachers()
        {
            return View();
        }
        public IActionResult Schedule(string group = "ИТ-21")
        {
            var schedules = GetSampleSchedules();
            var groups = new List<string> { "ИТ-21", "ИТ-22", "ЭК-21", "ЮР-21", "ФЛ-21" };

            var viewModel = new ScheduleViewModel
            {
                Schedules = schedules.Where(s => s.Group == group).ToList(),
                SelectedGroup = group,
                Groups = groups
            };

            return View("Schedules", viewModel); // Явно указываем имя файла с маленькой буквы
        }
        public IActionResult Materials()
        {
            var materials = GetSampleMaterials();
            return View(materials);
        }
        public IActionResult Grades()
        {
            var grades = GetSampleGrades();
            return View(grades);
        }
        public IActionResult News()
        {
            var news = GetSampleNews();
            return View(news);
        }

        private List<NewsItem> GetSampleNews()
        {
            return new List<NewsItem>
    {
        new NewsItem { Id = 1, Title = "День открытых дверей", Content = "Приглашаем всех желающих на день открытых дверей, который состоится 15 февраля.", Date = "10.01.2025", Category = "Мероприятие" },
        new NewsItem { Id = 2, Title = "Научная конференция", Content = "Университет проводит международную научную конференцию по информационным технологиям.", Date = "12.01.2025", Category = "Наука" },
        new NewsItem { Id = 3, Title = "Обновление расписания", Content = "Внесены изменения в расписание занятий на второй семестр.", Date = "15.01.2025", Category = "Учебный процесс" },
        new NewsItem { Id = 4, Title = "Студенческая олимпиада", Content = "Принимаются заявки на участие в студенческой олимпиаде по программированию.", Date = "18.01.2025", Category = "Мероприятие" },
        new NewsItem { Id = 5, Title = "Каникулы", Content = "Напоминаем о зимних каникулах с 25 января по 2 февраля.", Date = "20.01.2025", Category = "Учебный процесс" },
        new NewsItem { Id = 6, Title = "Новые лаборатории", Content = "Открыты новые компьютерные лаборатории для практических занятий.", Date = "22.01.2025", Category = "Инфраструктура" }
    };
        }
        private List<StudentGrade> GetSampleGrades()
        {
            return new List<StudentGrade>
    {
        new StudentGrade { Id = 1, Subject = "Программирование", Grade = 5, Date = "15.01.2025", Type = "Лабораторная работа", Teacher = "Петрова С.М." },
        new StudentGrade { Id = 2, Subject = "Математика", Grade = 4, Date = "18.01.2025", Type = "Контрольная работа", Teacher = "Иванов А.П." },
        new StudentGrade { Id = 3, Subject = "Базы данных", Grade = 5, Date = "20.01.2025", Type = "Практическая работа", Teacher = "Кузнецова О.Л." },
        new StudentGrade { Id = 4, Subject = "Физика", Grade = 3, Date = "22.01.2025", Type = "Лабораторная работа", Teacher = "Сидоров В.К." },
        new StudentGrade { Id = 5, Subject = "Веб-разработка", Grade = 5, Date = "25.01.2025", Type = "Проект", Teacher = "Николаев Д.С." },
        new StudentGrade { Id = 6, Subject = "Иностранный язык", Grade = 4, Date = "28.01.2025", Type = "Тест", Teacher = "Смирнова Е.В." },
        new StudentGrade { Id = 7, Subject = "Алгоритмы", Grade = 5, Date = "30.01.2025", Type = "Экзамен", Teacher = "Федоров П.Н." },
        new StudentGrade { Id = 8, Subject = "Сети", Grade = 4, Date = "02.02.2025", Type = "Зачет", Teacher = "Васильев М.К." }
    };
        }

        private List<LearningMaterial> GetSampleMaterials()
        {
            return new List<LearningMaterial>
    {
        new LearningMaterial { Id = 1, Title = "Введение в программирование", Subject = "Программирование", Type = "Лекция", Size = "2.5 MB", UploadDate = "15.01.2025" },
        new LearningMaterial { Id = 2, Title = "Основы алгоритмов", Subject = "Алгоритмы", Type = "Презентация", Size = "1.8 MB", UploadDate = "18.01.2025" },
        new LearningMaterial { Id = 3, Title = "Базы данных. Лекция 1", Subject = "Базы данных", Type = "Лекция", Size = "3.2 MB", UploadDate = "20.01.2025" },
        new LearningMaterial { Id = 4, Title = "Лабораторная работа №1", Subject = "Программирование", Type = "Лабораторная", Size = "1.1 MB", UploadDate = "22.01.2025" },
        new LearningMaterial { Id = 5, Title = "Математический анализ", Subject = "Математика", Type = "Учебник", Size = "5.7 MB", UploadDate = "25.01.2025" },
        new LearningMaterial { Id = 6, Title = "Веб-разработка", Subject = "Веб-разработка", Type = "Практикум", Size = "2.3 MB", UploadDate = "28.01.2025" },
        new LearningMaterial { Id = 7, Title = "Физика. Механика", Subject = "Физика", Type = "Лекция", Size = "4.1 MB", UploadDate = "30.01.2025" }
    };
        }
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

    }
}
