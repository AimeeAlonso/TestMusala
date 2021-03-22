using Domain;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IDeviceRepository:IRepository<Device>
    {
        int GetDeviceCount(int gatewayId);
        IEnumerable<Device> GetAllByGateway(int gatewayId);
    }
}
