using GarageSpace.NotificationService.Models;

namespace GarageSpace.NotificationService.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task AddNewUser(User user);
    }
}
