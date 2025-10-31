using Microsoft.EntityFrameworkCore;
using UniversityInformationSystem.Data;
using UniversityInformationSystem.Models;

namespace UniversityInformationSystem.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string email, string password);
        Task<User?> Register(User user);
        Task<bool> UserExists(string email, string studentId);
    }

    public class UserService : IUserService
    {
        private readonly UniversityContext _context;

        public UserService(UniversityContext context)
        {
            _context = context;
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.Faculty)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                user.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User?> Register(User user)
        {
            // Проверяем, нет ли уже пользователя с таким email или studentId
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.StudentId == user.StudentId);

            if (existingUser != null)
            {
                return null; // Пользователь уже существует
            }

            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string email, string studentId)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email || u.StudentId == studentId);
        }
    }
}