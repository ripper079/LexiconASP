using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
