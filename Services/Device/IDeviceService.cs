using Domain;
using Services.Gateway.Dtos;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Represent a base contract of business implementations.
    /// Here we have a base resources for optimize code reusing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeviceService : IService<Device>
    {
        Task<Result> AddDevice(DeviceDto device);


    }
}