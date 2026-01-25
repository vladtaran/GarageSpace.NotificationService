using GarageSpace.NotificationService.Models.Domain;
using GarageSpace.NotificationService.Repository.Infrastructure;
using GarageSpace.NotificationService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GarageSpace.NotificationService.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly MainDbContext _context;
        public UserRepository(MainDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(user));
            }

            // Check if user with same email already exists
            var existingUser = await GetByEmailAsync(user.Email);
            if (existingUser != null)
                throw new InvalidOperationException($"User with email {user.Email} already exists.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity?> GetUserById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        private async Task<UserEntity?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
