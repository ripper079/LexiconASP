using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class DeleteRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
