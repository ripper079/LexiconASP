using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentMVC.Models
{
    public class City
    {
        [Key]
        public int IdCity { get; set; }

        //public int Country_ID { get; set; }
        //[Required]
        public string CityName { get; set; }

        public List<Person> listOfPersons;
    }
}
