using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class RestaurantService
    {
        RestaurantRepo repo;
        IMapper mapper;
        public RestaurantService(RestaurantRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<RestaurantModel> GetAll()
        {
            var data = repo.GetAll();
            return mapper.Map<List<RestaurantModel>>(data);
        }

        public RestaurantModel Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<RestaurantModel>(data);
        }

        public bool Create(RestaurantModel restaurant)
        {
            var data = mapper.Map<Restaurant>(restaurant);
            return repo.Create(data);
        }

        public bool Update(RestaurantModel restaurant)
        {
            var data = mapper.Map<Restaurant>(restaurant);
            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}