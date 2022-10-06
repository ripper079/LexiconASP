using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class PersonViewModel
    {

        public int IdPerson { get; set; }
        [Required]
        public string FullName { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";        
        public int City_Id { get; set; }
    }
}
