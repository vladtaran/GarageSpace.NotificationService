using GarageSpace.Contracts;
using GarageSpace.NotificationService.Models;

namespace GarageSpace.NotificationService.Worker.Mappers
{
    public class UserFollowerMapper
    {
        public static UserFollower MapFromUserBlogFollowedEvent(UserBlogFollowedEvent evt) 
        {
            return new UserFollower
            {
                UserId = evt.UserId,
                FollowerUserId = evt.FollowerUserId
            };
        }
    }
}
