using GarageSpace.NotificationService.Models.Domain;
using GarageSpace.NotificationService.Repository.Interfaces;

namespace GarageSpace.NotificationService.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<UserEntity> CreateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
