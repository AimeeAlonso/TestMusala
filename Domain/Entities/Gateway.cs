using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Gateway : BaseEntity
    {
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IPV4Address { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
