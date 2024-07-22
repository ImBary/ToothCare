using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class AppointmentsCreateDTO
    {
        
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
