using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class PeopleViewModel
    {

        public List<Person> listOfPersons;        
        public CreatePersonViewModel cpvm { get; set; } = new CreatePersonViewModel();

        public PeopleViewModel() 
        {
            listOfPersons = new List<Person>();
            //Populate list with 'imaginary' Persons
            PopulateWithPersons();
        }
   
        //Initialize the list
        private void PopulateWithPersons() 
        {            
            //Aware that last parameter must be < 100 and here thay must be unique
            addPersonToList("Daniel Oikarainen", "033-0000000", "Borås", 1);
            addPersonToList("Marko Kiwi", "031-11111", "Göteborg", 2);
            addPersonToList("Donald Trumph", "011-222222", "WashingtonDC", 3);
            addPersonToList("Vladimir Putin", "007-3333333", "Moskva", 4);
            addPersonToList("Santa Kall", "123-12456", "Kiruna", 5);
            addPersonToList("Kalle Svensson", "033-3421", "Stockholm", 6);
            addPersonToList("Diego Maradona", "0054-444444", "Buenos Aires", 7);
            addPersonToList("Lionel Messi", "5554-444444", "Buenos Aires", 8);
            addPersonToList("Kalle Orvarsson", "132-1234", "Korvstaden", 9);
            addPersonToList("Daniel Sturesson", "4545-1678932", "Korvstaden", 10);
            addPersonToList("Olle Svensson", "222-121844", "Kiruna", 11);
            addPersonToList("Hanna Eriksson", "033-111111", "Stockholm", 12);
            addPersonToList("Kalle Svensson", "033-222222", "Borås", 13);
            addPersonToList("Hanna Eriksson", "041-123678", "Malmö", 14);
            addPersonToList("Glenn Hysen", "123-789-111", "Göteborg", 15);
            addPersonToList("Valrossen Ross", "0054-444444", "Bubbel havet",16);
            addPersonToList("Kalle Eriksson", "033-222222", "Borås", 17);
            addPersonToList("Kalle Svensson", "211-23125", "Strömstad", 18);
        }
        
        public void addPersonToList(string fullName, string phoneNumber, string city, int idForPerson)
        {
            listOfPersons.Add(
                new Person() 
                {
                    IdPerson = idForPerson,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    City = city
                }
            );
        }
        
        //Helper function
        public bool isIdPresent(int prospectId) 
        {
            foreach (Person person in listOfPersons) 
            {
                if (person.IdPerson == prospectId)
                {
                    return true;
                }
            }
            return false;
        }

        public void removePersonFromList(int validId) 
        {
            foreach(Person person in listOfPersons)
            {
                if (person.IdPerson == validId) 
                {
                    listOfPersons.Remove(person);
                    break;
                }
            }            
        }

        //Filters the original list(viewmodel). VERY vague description in assigment paper. Colleagues has 3 different Interpretations of "Search"
        public void filterList(string filterByFullName, string filterByPhoneNumber, string filterByCity) 
        {
            List<Person> resultingList = listOfPersons;            

            if (! String.IsNullOrEmpty(filterByFullName))
            {
                resultingList = resultingList.Where(item => String.Equals(item.FullName, filterByFullName)).ToList();
            }

            if (! String.IsNullOrEmpty(filterByPhoneNumber))
            {
                resultingList = resultingList.Where(item => String.Equals(item.PhoneNumber, filterByPhoneNumber)).ToList();
            }
            
            if (! String.IsNullOrEmpty(filterByCity))
            {
                resultingList = resultingList.Where(item => String.Equals(item.City, filterByCity)).ToList();
            }

            //New resulting filter
            listOfPersons = resultingList;
        }
       
        
        
    }
}
