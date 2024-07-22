using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class ProceduresDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        public string Description { get;  set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}
