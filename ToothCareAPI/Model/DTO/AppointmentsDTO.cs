using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class AppointmentsDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public DateTime DateOfAppoitment { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        public bool Done { get; set; }
        [Required]
        
        public int DoctorId { get; set; }
        [Required]
       
        public int ProcedureId { get; set; }
        [Required]

        public int ClientId { get; set; }

    }
}
