﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Gateway.Dtos
{
   public  class GatewayDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string IPV4Address { get; set; }
    }
}