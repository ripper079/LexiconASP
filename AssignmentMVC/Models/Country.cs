using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        //public int IdCountry { get; set; }

        public string CountryName { get; set; } = "";

        //Navigation Properties - Relationship between Countries and Cities (This creates a foreign key)        
        public List<City> Cities { get; set; } = new List<City>();

    }
}
