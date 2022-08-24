using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CreatePersonViewModel
    {
        [Display(Name ="Fullname")]
        [Required]
        [StringLength(40)]
        public string FullName { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "City")]
        [Required]
        [StringLength(35)]
        public string City { get; set; }
    }
}
