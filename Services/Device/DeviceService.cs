using AutoMapper;
using DataAccess.Repositories;
using Domain;
using Services.Utils;
using Services.Gateway.Dtos;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            if ((this._repository as IDeviceRepository).GetDeviceCount(device.GatewayId) >= 10)
            {
                result.AddError("The gateway has reached the devices limit. Please delete a device in order to add a new one");
            }
            else if (device.UId == 0 )
            {
                result.AddError("Please, specify a universal id.");
            }
            else if (device.Vendor == null || device.Vendor == "")
            {
                result.AddError("Please, specify a vendor.");
            }
            else if (this._repository.GetAll().Any(x=>x.UId==device.UId))
            {
                result.AddError("There is already a device with the specified unversal id");
            }
            else
            {
                try
                {
                    device.DateCreated = DateTime.Today;
                    await this._repository.Insert(_mapper.Map<Device>(device));
                }
                catch (System.Exception)
                {

                    result.AddError("Unexpected error adding a device");
                }
                 
            }
            return result;
        }
    }
}

