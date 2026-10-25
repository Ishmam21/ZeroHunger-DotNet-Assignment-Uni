using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class EmployeeRepo
    {
        ZeroHungerDbContext db;

        public EmployeeRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public List<Employee> GetAll()
        {
            return db.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        public bool Create(Employee model)
        {
            db.Employees.Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Update(Employee model)
        {
            var ex = Get(model.Id);
            ex.Name = model.Name;
            ex.Phno = model.Phno;
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
                return false;

            db.Employees.Remove(employee);
            return db.SaveChanges() > 0;
        }
    }

}