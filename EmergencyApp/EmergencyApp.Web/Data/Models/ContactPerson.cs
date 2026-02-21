namespace EmergencyApp.Web.Data.Models;

public class ContactPerson
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
}
