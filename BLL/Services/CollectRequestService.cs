using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class CollectRequestService
    {
        CollectRequestRepo repo;
        IMapper mapper;

        public CollectRequestService(CollectRequestRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<CollectRequestModel> GetAll()
        {
            var data = repo.GetAll();
            return mapper.Map<List<CollectRequestModel>>(data);
        }

        public CollectRequestModel Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<CollectRequestModel>(data);
        }

        public bool Create(CollectRequestModel model)
        {
            var data = mapper.Map<CollectRequest>(model);
            return repo.Create(data);
        }

        public bool Update(CollectRequestModel model)
        {
            var data = mapper.Map<CollectRequest>(model);
            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        // workflow-specific
        public bool Accept(int id, int empId)
        {
            return repo.Accept(id, empId);
        }

        public bool MarkCollected(int id)
        {
            return repo.MarkCollected(id);
        }

        public bool Complete(int id)
        {
            return repo.Complete(id);
        }

        public bool Reject(int id)
        {
            return repo.Reject(id);
        }

        public List<CollectRequestModel> GetByStatus(string status)
        {
            var data = repo.GetByStatus(status);
            return mapper.Map<List<CollectRequestModel>>(data);
        }

        public List<CollectRequestModel> GetByRestaurant(int resid)
        {
            var data = repo.GetByRestaurant(resid);
            return mapper.Map<List<CollectRequestModel>>(data);
        }

        public List<CollectRequestModel> GetByEmployee(int empid)
        {
            var data = repo.GetByEmployee(empid);
            return mapper.Map<List<CollectRequestModel>>(data);
        }

        public List<CollectRequestModel> GetUrgent()
        {
            var data = repo.GetUrgent();
            return mapper.Map<List<CollectRequestModel>>(data);
        }
    }
}