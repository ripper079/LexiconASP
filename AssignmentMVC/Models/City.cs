using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentMVC.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        //public int IdCity { get; set; }
                
        public string CityName { get; set; } = "";


        //Navigation properties and relationships
        //Relationship to Countries
        public int Country_Id { get; set; } //Make it nullable to allow field contain NULL
        //[Required]
        [ForeignKey("Country_Id")]
        public Country Country { get; set; }



        //Relationship to People/Persons        
        public List<Person> People { get; set; } = new List<Person>();
    }
}
