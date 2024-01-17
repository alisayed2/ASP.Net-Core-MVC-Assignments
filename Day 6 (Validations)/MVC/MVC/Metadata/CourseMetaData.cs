using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC.Metadata
{
    public class CourseMetaData
    {
        [MaxLength(10, ErrorMessage = "Course Name Length Must Be Less Than 10 Char ")]
        [MinLength(2, ErrorMessage = "Course Name Length Must Be More Than One Char ")]
        //[UniqueCourseName(ErrorMessage="Course Name Already Exist From Model")] // Error message not work because castum validation work only with server side
        [Remote("CourseNameValidation", "Course", ErrorMessage = "Course Name Already Exist")] // Remote Work AJAX Call That Is Means It Work With ClientSide
        public string Name { get; set; }

        [Remote("MinDegreeValidation", "Course", AdditionalFields = "Degree", ErrorMessage = "Course Min Degree Must Be Less Than Degree")]
        public int MinDegree { get; set; }

        [Range(50, 100)]
        public int Degree { get; set; }
    }
}
