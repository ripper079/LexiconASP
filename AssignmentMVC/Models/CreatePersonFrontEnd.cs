using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CreatePersonFrontEnd
    {
        [Display(Name = "Fullname")]
        [Required]
        [StringLength(40)]
        public string FullName { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        //[Display(Name = "Country ID")]
        //[Required]
        public int CountryId { get; set; }

        //[Display(Name = "City ID")]
        //[Required]
        public int CityId { get; set; }
    }
}
