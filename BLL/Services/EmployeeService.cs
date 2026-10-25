using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class EmployeeService
    {
        EmployeeRepo repo;
        IMapper mapper;
        public EmployeeService(EmployeeRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<EmployeeModel> GetAll()
        {
            var data = repo.GetAll();
            return mapper.Map<List<EmployeeModel>>(data);
        }

        public EmployeeModel Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<EmployeeModel>(data);
        }

        public bool Create(EmployeeModel employee)
        {
            var data = mapper.Map<Employee>(employee);
            return repo.Create(data);
        }

        public bool Update(EmployeeModel employee)
        {
            var data = mapper.Map<Employee>(employee);
            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}