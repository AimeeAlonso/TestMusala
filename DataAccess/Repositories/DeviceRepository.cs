using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class DeviceRepository:BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository( TestDbContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<Device> GetAllByGateway(int gatewayId)
        {
            return this._entities.Where(s => s.GatewayId==gatewayId);
        }
        public int GetDeviceCount(int gatewayId)
        {
            return GetAllByGateway(gatewayId).Count();
        }
    }
}
