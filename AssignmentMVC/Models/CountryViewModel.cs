using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CountryViewModel
    {
        //public List<Country> listOfCountries;
        //public List<Country> listOfCountries = new List<Country>();

        public int Id { get; set; }

        [Required]        
        public string CountryName { get; set; }

    }
}
