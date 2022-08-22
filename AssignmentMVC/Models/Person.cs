namespace AssignmentMVC.Models
{
    public class Person
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

        public Person(string fullName, string phoneNumber, string city)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            City = city;
        }

        public override string ToString()
        {
            return $"[{FullName}] | [{PhoneNumber}] | [{City}]";
        }
    }
}
