namespace EmergencyApp.Web.Data.Models;

public class UserAddress
{
    public int Id { get; set; }
    public required string AddressString { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
