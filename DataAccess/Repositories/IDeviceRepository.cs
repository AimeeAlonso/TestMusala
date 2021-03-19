using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public interface IDeviceRepository:IRepository<Device>
    {
        int GetDeviceCount(int gatewayId);
        IEnumerable<Device> GetAllByGateway(int gatewayId);
    }
}
