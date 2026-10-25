using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(11)]
        public string Phno { get; set; } = null!;
    }

}
