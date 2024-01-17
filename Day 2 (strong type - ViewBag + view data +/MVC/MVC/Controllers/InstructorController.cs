using Microsoft.AspNetCore.Mvc;
using MVC.Entities;

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
    }
}
