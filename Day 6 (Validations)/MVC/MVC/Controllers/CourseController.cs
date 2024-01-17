using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Models;
namespace MVC.Controllers
{
    public class CourseController : Controller
    {
        ITIContext db = new ITIContext();
        public IActionResult Index()
        {
            var coursesData = db.Courses.ToList();
            var depts = db.Departments.ToList();
            ViewData["depts"] = depts;
            return View(coursesData);
        }

        public IActionResult AddNewCourse()
        {
            var depts = db.Departments.ToList();
            ViewData["depts"] = depts;
            return View();
        }

        [HttpPost]
        public IActionResult SaveNewCourse(Course course)
        {
            //if (course.Name == null) for test
            if(!ModelState.IsValid)
            {
                var depts = db.Departments.ToList();
                ViewData["depts"] = depts;
                return View("AddNewCourse", course);
            }
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Remote Validation For Unique Course Name
        public IActionResult CourseNameValidation (string Name)
        {
            Course course = db.Courses.FirstOrDefault(c=>c.Name == Name);
            if(course != null)
            {
                return Json(false);
            }
            return Json(true);
        }

        // Remote Validation For Compare Min Degree With Degree

        public IActionResult MinDegreeValidation (int MinDegree,int Degree)
        {
            if(MinDegree >= Degree)
            {
                return Json(false);
            }

            return Json(true);
        }

    }
}
