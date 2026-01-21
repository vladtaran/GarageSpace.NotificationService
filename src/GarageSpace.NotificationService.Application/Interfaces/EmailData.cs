
namespace GarageSpace.NotificationService.Application.Interfaces
{
    public class EmailData
    {
        public long UserId { get; set; }
        public long FollowerUserId { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}