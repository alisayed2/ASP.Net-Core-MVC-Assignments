using MVC.Entities;
using MVC.Models;

namespace MVC.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        ITIContext context; // = new ITIContext();

        public DepartmentRepository(ITIContext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void Insert(Department department)
        {
            // any validations put them in controller
            context.Add(department);
            context.SaveChanges();
        }

        public void Update(Department department)//(int id, Course course)
        {
            //Course oldCourse = GetById(id);
            //oldCourse.Name = course.Name;
            //oldCourse.Degree = course.Degree;
            //oldCourse.MinDegree = course.MinDegree;
            //oldCourse.DepartmentId = course.DepartmentId;
            context.Update(department);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            //Course Course = GetById(id);
            context.Departments.Remove(GetById(id));
            context.SaveChanges();
        }
    }
}
