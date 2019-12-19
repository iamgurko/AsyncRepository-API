using System;

namespace Entities.DataTransferObjects
{
    public class MeterDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string MeterType { get; set; }
    }
}
