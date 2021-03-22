using Domain;
using Services.Gateway.Dtos;
using Services.Utils;
using System.Threading.Tasks;

namespace Services
{
  
    public interface IDeviceService : IService<Device>
    {
        Task<Result> AddDevice(DeviceDto device);
    }
}