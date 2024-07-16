using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToothCareAPI.Model
{
    public class Procedures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.01,double.MaxValue)]
        public double Price { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}
