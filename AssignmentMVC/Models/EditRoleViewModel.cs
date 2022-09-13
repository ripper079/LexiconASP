using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
