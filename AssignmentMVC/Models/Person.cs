using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentMVC.Models
{
    public class Person
    {
        [Key]
        public int IdPerson { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        //public string City { get; set; }

        //Navigation properties
        public int? City_Id { get; set; }
        //[Required]
        [ForeignKey("City_Id")]
        public City CityOfPerson { get; set; }



        //Printing the current state of an object
        public override string ToString()
        {
            return $"[{FullName}] | [{PhoneNumber}] | [{CityOfPerson.CityName}] | [{CityOfPerson.Id}]";
        } 
    }
}
