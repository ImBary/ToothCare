using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class ProceduresUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}
