using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public static class BdInitializer
    { 
         public static void Initialize(TestDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Gateways.Any())
            {
                return;  
            }
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var gateways = new Gateway[]
            {
               new Gateway{ SerialNumber="G001",Name="Gateway 1",IPV4Address="192.168.10.1"},
               new Gateway{ SerialNumber="G002",Name="Gateway 2",IPV4Address="192.168.10.2"},
               new Gateway{ SerialNumber="G003",Name="Gateway 3",IPV4Address="192.168.10.3"},
               new Gateway{ SerialNumber="G004",Name="Gateway 4",IPV4Address="192.168.10.4"},

            };
            var devices = new Device[]
           {
                new Device{UId=1, Vendor="Vendor 1",DateCreated=new DateTime(2020,02,05),Status=true,GatewayId=1},
                new Device{UId=2, Vendor="Vendor 2",DateCreated=new DateTime(2021,04,05),Status=false,GatewayId=1},
                new Device{UId=3, Vendor="Vendor 3",DateCreated=new DateTime(2020,08,25),Status=false,GatewayId=1},
                new Device{UId=4, Vendor="Vendor 4",DateCreated=new DateTime(2020,10,16),Status=true,GatewayId=1},
                new Device{UId=5, Vendor="Vendor 5",DateCreated=new DateTime(2020,11,09),Status=true,GatewayId=1},
                new Device{UId=6, Vendor="Vendor 6",DateCreated=new DateTime(2020,05,16),Status=true,GatewayId=1},
                new Device{UId=7, Vendor="Vendor 7",DateCreated=new DateTime(2019,02,12),Status=false,GatewayId=1},
                new Device{UId=8, Vendor="Vendor 8",DateCreated=new DateTime(2019,04,23),Status=true,GatewayId=1},
                new Device{UId=9, Vendor="Vendor 9",DateCreated=new DateTime(2010,02,14),Status=true,GatewayId=1},
                new Device{UId=10, Vendor="Vendor 10",DateCreated=new DateTime(2020,06,03),Status=false,GatewayId=1},
                new Device{UId=11, Vendor="Vendor 11",DateCreated=new DateTime(2020,09,19),Status=false,GatewayId=2},
                new Device{UId=12, Vendor="Vendor 12",DateCreated=new DateTime(2020,12,05),Status=false,GatewayId=2},
                new Device{UId=13, Vendor="Vendor 13",DateCreated=new DateTime(2020,01,25),Status=true,GatewayId=2}
           };
            foreach (Gateway g in gateways)
            {
                context.Gateways.Add(g);
            }
            context.SaveChanges();


            foreach (Device d in devices)
            {
                context.Devices.Add(d);
            }
            context.SaveChanges();

        }
    }
}
