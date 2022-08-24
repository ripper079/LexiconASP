using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AssignmentMVC.Models
{
    public class Person
    {
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
