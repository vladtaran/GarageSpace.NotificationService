using GarageSpace.Contracts;
using GarageSpace.NotificationService.Models;
using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Worker.Mappers;
using MassTransit;

namespace GarageSpace.NotificationService.Worker.Consumers
{
    public class UserEventsConsumer : IConsumer<UserRegistered>
    {
        private readonly IUserManagementService _userManagementService;
        public UserEventsConsumer(IUserManagementService userManagementService) 
        {
            _userManagementService = userManagementService;
        }
        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            try
            {
                User user = UserMapper.MapFromUserRegistered(context.Message);

                await _userManagementService.AddNewUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
