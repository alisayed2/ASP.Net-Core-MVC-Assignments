using MVC.Entities;
using MVC.Models;

namespace MVC.Repository
{
    public class CourseRepository : ICourseRepository
    {
        ITIContext context; // = new ITIContext();
        
        public CourseRepository(ITIContext _context)
        {
            context = _context;
        }
        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public Course GetByName(string name)
        {
            return context.Courses.FirstOrDefault(c => c.Name == name);
        }
        public void Insert(Course course)
        {
            // any validations put them in controller
            context.Add(course);
            context.SaveChanges();
        }

        public void Update (Course course)//(int id, Course course)
        {
            //Course oldCourse = GetById(id);
            //oldCourse.Name = course.Name;
            //oldCourse.Degree = course.Degree;
            //oldCourse.MinDegree = course.MinDegree;
            //oldCourse.DepartmentId = course.DepartmentId;
            context.Update(course);
            context.SaveChanges();
        }

        public void Delete (Course course) //(int id)
        {
            //Course Course = GetById(id);
            //context.Courses.Remove(GetById(id));
            context.Courses.Remove(course);
            context.SaveChanges();
        }
    }
}
