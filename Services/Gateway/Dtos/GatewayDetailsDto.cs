using System.Collections.Generic;

namespace Services.Gateway.Dtos
{
   public  class GatewayDetailsDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string IPV4Address { get; set; }
        public IEnumerable<DeviceDto> Devices { get; set; }
    }
}
