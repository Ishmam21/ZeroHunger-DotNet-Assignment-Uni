using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class UserRepo
    {
        ZeroHungerDbContext db;

        public UserRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public User? GetByUsername(string username)
        {
            return db.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetById(int id)
        {
            return db.Users.Find(id);
        }

        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Any(u => u.Username == username);
        }
    }
}
