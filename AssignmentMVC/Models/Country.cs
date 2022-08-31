using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }

        public string CountryName { get; set; }

        public List<City> listOfCities;

    }
}
