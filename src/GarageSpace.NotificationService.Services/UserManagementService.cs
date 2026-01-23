using GarageSpace.NotificationService.Models;
using GarageSpace.NotificationService.Repository.Interfaces;
using GarageSpace.NotificationService.Services.Interfaces;

namespace GarageSpace.NotificationService.Services
{
    public class UserManagementService : IUserManagementService
    {
        public UserManagementService(IUserRepository repository) { }

        public async Task AddNewUser(User user)
        {
            try 
            {
                //var user = 
            }
            catch (Exception e) 
            {
            }
        }
    }
}
