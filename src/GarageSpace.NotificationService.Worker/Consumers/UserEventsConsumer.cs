using GarageSpace.Contracts;
using GarageSpace.NotificationService.Models;
using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Worker.Mappers;
using MassTransit;

namespace GarageSpace.NotificationService.Worker.Consumers
{
    public class UserEventsConsumer : IConsumer<UserCreated>
    {
        private readonly IUserManagementService _userManagementService;
        public UserEventsConsumer(IUserManagementService userManagementService) 
        {
            _userManagementService = userManagementService;
        }
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            try
            {
                User user = UserMapper.MapFromUserCreated(context.Message);

                await _userManagementService.AddNewUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
