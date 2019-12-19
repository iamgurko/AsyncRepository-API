using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("owner")] 
    public class Owner 
    { 
        [Column("OwnerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "İsim Gerekli")] 
        [StringLength(60, ErrorMessage = "60 Karakterden fazla olmamalıdır")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gereklli")] 
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Adres alanı gerekli")] 
        [StringLength(200, ErrorMessage = "Adres 200 karakterden fazla olmamalıdır")] 
        public string Address { get; set; }

        public ICollection<Meter> Meters { get; set; }
    }
}
