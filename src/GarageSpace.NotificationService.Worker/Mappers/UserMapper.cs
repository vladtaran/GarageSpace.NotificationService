using GarageSpace.Contracts;
using GarageSpace.NotificationService.Models;

namespace GarageSpace.NotificationService.Worker.Mappers
{
    public class UserMapper
    {
        public static User MapFromUserCreated(UserCreated evt) 
        {
            return new User
            {
                Id = evt.UserId,
                Email = evt.Email,
                Name = evt.Name,
                Nickname = evt.Nickname
            };
        }
    }
}
