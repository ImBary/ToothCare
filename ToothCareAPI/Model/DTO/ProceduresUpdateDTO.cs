using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class ProceduresUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}
