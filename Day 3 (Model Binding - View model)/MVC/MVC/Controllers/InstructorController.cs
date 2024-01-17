using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Models;

namespace MVC.Controllers
{
    public class InstructorController : Controller
    {
        
        ITIContext db = new ITIContext();
        public IActionResult Index()
        {
            var insts = db.Instructors.ToList();
            //var insts = db.Instructors.Select(i => new {i.Id,i.Name,i.Image,i.Salary,i.Address,i.CourseId,DepartmentName = i.Department.Name} );
            return View(insts);
        }

        public IActionResult instructorDetails(int id)
        {
            var inst = db.Instructors.FirstOrDefault(i => i.Id == id);
            return View(inst);
        }

        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveNew(Instructor instructor)
        {
            if (instructor.Name == null)
            {
                 return View("AddNew", instructor);
            }
            db.Instructors.Add(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return RedirectToAction("/Instructor/Index");
        }
    }
}
