using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
