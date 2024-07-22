using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class DoctorsDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Specialty { get; set; }
    }
}
