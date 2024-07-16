using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToothCareAPI.Model
{
    public class Clients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Appointments> Appointments { get; set; }

    }
}
