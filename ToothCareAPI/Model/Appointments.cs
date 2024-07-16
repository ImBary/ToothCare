using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToothCareAPI.Model
{
    public class Appointments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public DateTime DateOfAppoitment { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        [Range (0.01,double.MaxValue)]
        public double Price { get; set; }
        [Required]
        public bool Done { get; set; }
        [Required]
        [ForeignKey("Doctors")]
        public int DoctorId { get; set; }
        [Required]
        [ForeignKey("Procedures")]
        public int ProcedureId { get; set; }
        [Required]
        [ForeignKey("Clients")]
        public int ClientId { get; set; }

        public virtual Doctors Doctors { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Procedures Procedures { get; set; }
    }
}
