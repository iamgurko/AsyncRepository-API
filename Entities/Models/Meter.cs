using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("meter")] 
    public class Meter 
    {
        [Column("MeterId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Oluşturma tarihi gerekli")] 
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Hesap tipi gerekli")] 
        public string MeterType { get; set; }

        [Required(ErrorMessage = "Meter Id gerekli")] 

        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
