using MVC.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVC.Vaildators
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ITIContext db = new ITIContext();
            string name = (string)value;
            var course = db.Courses.FirstOrDefault(c=>c.Name==name);
            if (course != null)
            {
                return new ValidationResult("Course Name Already Exist");
            }
            return ValidationResult.Success;
        }
    }
}
