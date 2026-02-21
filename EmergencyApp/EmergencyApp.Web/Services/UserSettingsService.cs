using EmergencyApp.Web.Data;
using EmergencyApp.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.Web.Services;

public class UserSettingsService(ApplicationDbContext db)
{
    public async Task<(UserAddress? address, ContactPerson? contactPerson)> LoadAsync(string userId)
    {
        var settings = await db.UserSettings
            .AsNoTracking()
            .Include(us => us.Address)
            .Include(us => us.ContactPerson)
            .FirstOrDefaultAsync(us => us.UserId == userId);

        return (settings?.Address, settings?.ContactPerson);
    }

    public async Task SaveAddressAsync(string userId, UserAddress address)
    {
        var settings = await db.UserSettings
            .Include(us => us.Address)
            .FirstOrDefaultAsync(us => us.UserId == userId);

        if (settings is null)
        {
            settings = new UserSettings { UserId = userId };
            db.UserSettings.Add(settings);
        }

        if (settings.Address is null)
        {
            settings.Address = new UserAddress
            {
                AddressString = address.AddressString,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
        }
        else
        {
            settings.Address.AddressString = address.AddressString;
            settings.Address.Latitude = address.Latitude;
            settings.Address.Longitude = address.Longitude;
        }

        await db.SaveChangesAsync();
    }

    public async Task SaveContactPersonAsync(string userId, ContactPerson contactPerson)
    {
        var settings = await db.UserSettings
            .Include(us => us.ContactPerson)
            .FirstOrDefaultAsync(us => us.UserId == userId);

        if (settings is null)
        {
            settings = new UserSettings { UserId = userId };
            db.UserSettings.Add(settings);
        }

        if (settings.ContactPerson is null)
        {
            settings.ContactPerson = new ContactPerson
            {
                FullName = contactPerson.FullName,
                PhoneNumber = contactPerson.PhoneNumber
            };
        }
        else
        {
            settings.ContactPerson.FullName = contactPerson.FullName;
            settings.ContactPerson.PhoneNumber = contactPerson.PhoneNumber;
        }

        await db.SaveChangesAsync();
    }
}
