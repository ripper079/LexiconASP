using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CountryViewModel
    {
        //public List<Country> listOfCountries;
        //public List<Country> listOfCountries = new List<Country>();

        public int Id { get; set; }

        [Required]
        [Display(Name = "ENTER the name of the country")]
        public string CountryName { get; set; }

    }
}
