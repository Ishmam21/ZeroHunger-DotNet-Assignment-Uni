using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class CollectRequestModel
    {
        public int Id { get; set; }

        [Required]
        public int Resid { get; set; }

        public int? Empid { get; set; }

        [Required] 
         [RegularExpression("^(Pending|Accepted|Completed|Rejected)$",
            ErrorMessage = "Status must be one of: Pending, Accepted, Completed, Rejected")]
        public string Status { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime,ErrorMessage ="format: yyyy-MM-ddTHH:mm:ss, e.g. 2026-08-25T18:00:00")]
        public DateTime Maxtime { get; set; }

    }

}
