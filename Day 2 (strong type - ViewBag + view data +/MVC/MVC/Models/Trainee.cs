using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Grade {  get; set; }
        public virtual ICollection<CrsResult> CrsResults { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}
