using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class DoctorsCreateDTO
    {
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
