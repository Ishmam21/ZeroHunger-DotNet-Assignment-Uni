using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class RestaurantRepo
    {
        ZeroHungerDbContext db;

        public RestaurantRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public List<Restaurant> GetAll()
        {
            return db.Restaurants.ToList();
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.Find(id);
        }

        public bool Create(Restaurant model)
        {
            db.Restaurants.Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Update(Restaurant model)
        {
            var ex = Get(model.Id);
            if (ex == null)
                return false;
            ex.Name = model.Name;
            ex.Address = model.Address;
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
                return false;

            db.Restaurants.Remove(restaurant);
            return db.SaveChanges() > 0;
        }
    }

}