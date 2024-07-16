using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class AppointmentsUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        
        public string Number { get; set; }
        
        public DateTime DateOfAppoitment { get; set; }
        
        public string Room { get; set; }
        
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
       
        public bool Done { get; set; }
        

        public int DoctorId { get; set; }
       

        public int ProcedureId { get; set; }
        

        public int ClientId { get; set; }
    }
}
