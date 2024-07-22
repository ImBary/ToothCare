using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class ProceduresCreateDTO
    {
       
        [Required]
        public string Name { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}
