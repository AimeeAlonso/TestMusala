using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class DeviceRepository:BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository( IConfiguration config) : base( config)
        {
        }
    }
}
