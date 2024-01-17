using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Models;

namespace MVC.Controllers
{
    public class InstructorController : Controller
    {

        ITIContext db ;
        public InstructorController (ITIContext _db)
        {
            db= _db;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var insts = db.Instructors.ToList();
            //var insts = db.Instructors.Select(i => new {i.Id,i.Name,i.Image,i.Salary,i.Address,i.CourseId,DepartmentName = i.Department.Name} );
            return View(insts);
        }


        //cant reach these action without this route
        //[Route("ITIInstructor/{id}")] 
        //[Route("ITI/{id}")] 
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


        public IActionResult Update([FromRoute] int id)
        {
            var depts = db.Departments.ToList();
            ViewData["depts"] = depts;
            var instructor = db.Instructors.FirstOrDefault(i => i.Id == id);
            return View(instructor);
        }


        [HttpPost]

        public IActionResult SaveUpdate([FromRoute] int id, Instructor instructor)
        {
            if(instructor.Name == null)
            {
                var depts = db.Departments.ToList();
                ViewData["depts"] = depts;
                return View("Update",instructor);
            }
            db.Instructors.Update(instructor); //Core 7 --Id not Exist => Add | Id Exist => Update 
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete([FromRoute] int id, Instructor instructor)
        {
            if(instructor  == null)
            {
                return RedirectToAction("index");
            }
            db.Instructors.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("index");
        }


    }
}
