using Microsoft.AspNetCore.Identity;

namespace EmergencyApp.Web.Data.Models;

public class UserSettings
{
    public int Id { get; set; }

    public required string UserId { get; set; }
    public IdentityUser? User { get; set; }

    public int? AddressId { get; set; }
    public UserAddress? Address { get; set; }

    public int? ContactPersonId { get; set; }
    public ContactPerson? ContactPerson { get; set; }
}
