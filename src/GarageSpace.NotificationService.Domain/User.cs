namespace GarageSpace.NotificationService.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public required string Nickname { get; set; }
        public required string Email { get; set; }
    }
}
