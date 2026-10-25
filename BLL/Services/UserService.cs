using System.Security.Cryptography;
using System.Text;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class UserService
    {
        UserRepo repo;

        public UserService(UserRepo repo)
        {
            this.repo = repo;
        }

        // Returns the logged-in user DTO on success, null on failure
        public LoggedInUser? Login(string username, string password)
        {
            var user = repo.GetByUsername(username);
            if (user == null) return null;

            var hash = HashPassword(password);
            if (user.PasswordHash != hash) return null;

            return new LoggedInUser
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                LinkedId = user.LinkedId
            };
        }

        // Returns null on success, error message string on failure
        public string? Register(UserModel model)
        {
            if (repo.UsernameExists(model.Username))
                return "Username already taken.";

            var user = new User
            {
                Username = model.Username,
                PasswordHash = HashPassword(model.Password),
                Role = model.Role,
                LinkedId = model.LinkedId
            };

            return repo.Create(user) ? null : "Registration failed. Please try again.";
        }

        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
