using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class DoctorsUpdateDTO
    {
        [Required]
        public int Id { get; set; }
       
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Title { get; set; }
        
        [Range(1, 5)]
        public int Rate { get; set; }
        
        [Phone]
        public string Phone { get; set; }
        
        public string Specialty { get; set; }
    }
}
