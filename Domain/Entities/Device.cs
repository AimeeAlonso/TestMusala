using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Device : BaseEntity
    {
        [Required]
        public string Vendor { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public virtual int GatewayId { get; set; }
    }
}
