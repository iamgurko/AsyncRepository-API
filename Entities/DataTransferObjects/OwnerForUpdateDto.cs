using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class OwnerForUpdateDto
    {
        [Required(ErrorMessage = "İsim gerekli")]
        [StringLength(60, ErrorMessage = "İsim 60 karakterden fazla olmamalı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gerekli")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Adres gerekli")]
        [StringLength(100, ErrorMessage = "Adres 100 karakterden fazla olmamalı")]
        public string Address { get; set; }
    }
}
