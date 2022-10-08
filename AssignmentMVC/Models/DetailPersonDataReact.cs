namespace AssignmentMVC.Models
{
    public class DetailPersonDataReact
    {
        public int IdPerson { get; set; }
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Languages { get; set; } = "";
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
