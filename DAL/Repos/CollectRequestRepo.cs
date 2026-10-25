using DAL.EF;
using DAL.EF.Tables;


namespace DAL.Repos
{
    public class CollectRequestRepo
    {
        ZeroHungerDbContext db;

        public CollectRequestRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public List<CollectRequest> GetAll()
        {
            return db.CollectRequests.ToList();
        }

        public CollectRequest Get(int id)
        {
            return db.CollectRequests.Find(id);
        }

        

        public bool Update(CollectRequest model)
        {
            var ex = Get(model.Id);
            if (ex == null)
                return false;

            ex.Resid = model.Resid;
            ex.Empid = model.Empid;
            ex.Status = model.Status;
            ex.Maxtime = model.Maxtime;
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var collectRequest = db.CollectRequests.Find(id);
            if (collectRequest == null)
                return false;

            db.CollectRequests.Remove(collectRequest);
            return db.SaveChanges() > 0;
        }

      



        //workflow

        public bool Create(CollectRequest model)
        {
            model.Status = "Pending";
            model.Empid = null;
            db.CollectRequests.Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Accept(int id, int empId)
        {
            var ex = Get(id);
            if (ex == null)
                return false;

            ex.Empid = empId;
            ex.Status = "Accepted";
            return db.SaveChanges() > 0;
        }

        public bool MarkCollected(int id)
        {
            var ex = Get(id);
            if (ex == null)
                return false;

            ex.Status = "Collected";
            return db.SaveChanges() > 0;
        }

        public bool Complete(int id)
        {
            var ex = Get(id);
            if (ex == null)
                return false;

            ex.Status = "Completed";
            return db.SaveChanges() > 0;
        }

        public bool Reject(int id)
        {
            var ex = Get(id);
            if (ex == null)
                return false;

            ex.Status = "Rejected";
            return db.SaveChanges() > 0;
        }


        public List<CollectRequest> GetByStatus(string status)
        {
            return db.CollectRequests
                .Where(r => r.Status == status)
                .ToList();
        }

        public List<CollectRequest> GetByRestaurant(int resid)
        {
            return db.CollectRequests
                .Where(r => r.Resid == resid)
                .ToList();
        }

        public List<CollectRequest> GetByEmployee(int empid)
        {
            return db.CollectRequests
                .Where(r => r.Empid == empid)
                .ToList();
        }

        public List<CollectRequest> GetUrgent()
{
    return db.CollectRequests
        .Where(r => r.Status == "Pending")
        .OrderBy(r => r.Maxtime)
        .ToList();
}
    }
}