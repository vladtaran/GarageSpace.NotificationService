namespace GarageSpace.NotificationService.Models
{
    public class EmailData
    {
        public long UserId { get; set; }
        public long FollowerUserId { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}