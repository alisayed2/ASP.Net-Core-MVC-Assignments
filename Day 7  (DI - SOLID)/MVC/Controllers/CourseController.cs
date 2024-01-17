﻿using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Models;
using MVC.Repository;

namespace MVC.Controllers
{
    public class CourseController : Controller
    {
        // DIP
        ICourseRepository courseRepository;
        IDepartmentRepository departmentRepository;

        // Inject - ask
        public CourseController(ICourseRepository _crsRepository , IDepartmentRepository deptRepository )
        {
            // DI
            //courseRepository = new CourseRepository();
            //departmentRepository = new DepartmentRepository();
            courseRepository = _crsRepository;
            departmentRepository = deptRepository;

        }
        public IActionResult Index()
        {
            var coursesData = courseRepository.GetAll();
            var depts = departmentRepository.GetAll();
            ViewData["depts"] = depts;
            return View(coursesData);
        }

        public IActionResult AddNewCourse()
        {
            var depts = departmentRepository.GetAll();
            ViewData["depts"] = depts;
            return View();
        }

        [HttpPost]
        public IActionResult SaveNewCourse(Course course)
        {
            //if (course.Name == null) for test
            if(!ModelState.IsValid)
            {
                var depts = departmentRepository.GetAll();
                ViewData["depts"] = depts;
                return View("AddNewCourse", course);
            }
            courseRepository.Insert(course);
            return RedirectToAction("Index");
        }

        public IActionResult Update([FromRoute]int id)
        {
            var depts = departmentRepository.GetAll();
            ViewData["depts"] = depts;
            Course course = courseRepository.GetById(id);
            return View(course);
        }

        [HttpPost]
        public IActionResult SaveUpdate(Course course)
        {
            if (!ModelState.IsValid)
            {
                var depts = departmentRepository.GetAll();
                ViewData["depts"] = depts;
                return View("Update",course);
            }
            courseRepository.Update(course);
            return RedirectToAction("Index");
        }

        //public IActionResult Delete([FromRoute]int id,Course course)
        public IActionResult Delete(Course course)
        {
            //courseRepository.Delete(id);
            courseRepository.Delete(course);
            return RedirectToAction("index");
        }


        // Remote Validation For Unique Course Name
        public IActionResult CourseNameValidation (string name)
        {
            Course course = courseRepository.GetByName(name);
            if(course != null)
            {
                return Json(false);
            }
            return Json(true);
        }

        // Remote Validation For 

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
