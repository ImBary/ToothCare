using System.ComponentModel.DataAnnotations;

namespace ToothCareAPI.Model.DTO
{
    public class ClientsUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public bool Gender { get; set; }
        
        public string Pesel { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
    }
}
