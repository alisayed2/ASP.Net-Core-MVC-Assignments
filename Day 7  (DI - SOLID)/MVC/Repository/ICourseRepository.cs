using MVC.Models;

namespace MVC.Repository
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course GetById(int id);
        Course GetByName(string name);
        void Insert(Course course);
        void Update(Course course);
        void Delete(Course course);
    }
}
