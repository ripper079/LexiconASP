using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class Person
    {
        [Key]
        public int IdPerson { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"[{FullName}] | [{PhoneNumber}] | [{City}]";
        } 
    }
}
