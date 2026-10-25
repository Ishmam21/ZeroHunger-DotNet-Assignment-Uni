using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;
    }

}
