using System;

namespace Services.Gateway.Dtos
{
   public  class DeviceDto
    {
        public int Id { get; set; }
        public int UId { get; set; }
        public string Vendor { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public int GatewayId { get; set; }
    }
}
