using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class ProceduresDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}
