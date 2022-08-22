using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class PeopleViewModel
    {
        public List<Person> listOfPersons;
        public string FullName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        /*public Person person = new Person("foo", "foo2", "foo3");*/

        public PeopleViewModel() 
        {
            //Populate with experminatal data
            listOfPersons = new List<Person>();

            PopulateWithPersons();
        }

        private void PopulateWithPersons() 
        {
            listOfPersons.Add(new Person("Daniel Oikarainen", "033-000000", "Borås"));
            listOfPersons.Add(new Person("Marko Kiwi", "031-11111", "Göteborg"));
            listOfPersons.Add(new Person("Donald Trumph", "011-222222", "WashingtonDC"));
            listOfPersons.Add(new Person("Vladimir Putin", "007-3333333", "Moskva"));
            listOfPersons.Add(new Person("Diego Maradona", "0054-444444", "Argentina"));        
        }

        public List<Person> GetAllPersons() 
        {
            return listOfPersons;
        }

        public void AddPersonToList(Person aPerson) 
        {
            listOfPersons.Add(aPerson);
        }

        //Make sure that the lis is not empty
        public Person GetFirstPersonInList() 
        {
            return listOfPersons[0];
        }

        public Person GetLastPersonInList()
        {
            return listOfPersons[listOfPersons.Count - 1];
        }

        public int GetNumberOfPersonInList() 
        {
            return listOfPersons.Count;
        }
    }
}
