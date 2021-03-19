using AutoMapper;
using DataAccess.Repositories;
using Domain;
using Domain.Utils;
using Services.Gateway.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public  class DeviceService : BaseService<Device>, IDeviceService
    {
        public DeviceService(IDeviceRepository repository, IMapper mapper):base(repository, mapper)
        {
           
        }
        public async Task<Result> AddDevice(DeviceDto device)
        {
            Result result = new Result();
            if ((this._repository as IDeviceRepository).GetDeviceCount(device.GatewayId)>=10)
            {
                result.AddError("The gateway has already reached the devices limit");
            }
            else
            {
                result = await this._repository.Insert(_mapper.Map<Device>(device));
            }
            return result;
        }
    }
}

