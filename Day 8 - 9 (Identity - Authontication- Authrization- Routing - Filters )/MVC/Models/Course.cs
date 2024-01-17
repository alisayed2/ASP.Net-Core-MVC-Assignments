using Microsoft.AspNetCore.Mvc;
using MVC.Metadata;
using MVC.Vaildators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [ModelMetadataType(typeof(CourseMetaData))] // Tack all validation data annotation to meta data class and use this Property 
    // To Not Need To Repeat It If I Want it Again Or I Need It For View Model (Dont Need To Repeat It Again In View Model Bacause This Is Must) 
    public class Course
    {
        public  int Id { get; set; }
        //[StringLength(20)] Effect in both database and back end use it because it convert to varchar(max) = 2gb
        //[Required] // data type without null operator ? by default Not Null
        //[MaxLength(10,ErrorMessage ="Course Name Length Must Be Less Than 10 Char ")]
        //[MinLength(2, ErrorMessage = "Course Name Length Must Be More Than One Char ")]
        //[UniqueCourseName(ErrorMessage="Course Name Already Exist From Model")] // Error message not work because castum validation work only with server side
                                                                                //but work with  asp-validation-summary="All" Div
        ///[Remote("CourseNameValidation","Course",ErrorMessage ="Course Name Already Exist")] // Remote Work AJAX Call That Is Means It Work With ClientSide
        public string Name { get; set; } 
        public int Degree  { get; set; }
        //[Remote("MinDegreeValidation", "Course", AdditionalFields = "Degree", ErrorMessage = "Course Min Degree Must Be Less Than Degree")]
        public int MinDegree  { get; set; }
        public virtual ICollection<Instructor>? Instructors { get; set; } // Nullable Because IsValid Return Invalid
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }   // Nullable Because IsValid Return Invalid

    }
}
