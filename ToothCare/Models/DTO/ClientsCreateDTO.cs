﻿using System.ComponentModel.DataAnnotations;

namespace ToothCare.Models.DTO
{
    public class ClientsCreateDTO
    {
     
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
    }
}
