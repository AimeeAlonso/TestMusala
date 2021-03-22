using AutoMapper;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Services.Gateway.Dtos;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Services.Test
{
   
    public class DeviceServiceTests
    {
        IDeviceRepository deviceRepositoryMock = Substitute.For<IDeviceRepository>();
        IMapper mapper;
        public DeviceServiceTests()
        {
            ConfigureData();
        }
        private Domain.Device device;
        private DeviceDto deviceDto;
        private void ConfigureData() 
        {
             device = new Domain.Device()
            {
                Id = 1,
                Vendor = "V1",
                Status = true,
                DateCreated = DateTime.Today,
                UId=1,
                GatewayId=1

            };
            deviceDto = new DeviceDto()
            {
                Id = 1,
                Vendor = "V1",
                Status = true,
                DateCreated = DateTime.Today,
                UId = 1,
                GatewayId = 1

            };
           
            mapper = new MapperConfiguration(c => c.AddProfile<AutoMapping>()).CreateMapper();
            deviceRepositoryMock.Get(device.Id).Returns(Task<Domain.Device>.FromResult(device));
            deviceRepositoryMock.Insert(device).Returns(Task<Domain.Device>.FromResult(device));
            deviceRepositoryMock.GetDeviceCount(1).Returns(5);
            deviceRepositoryMock.GetDeviceCount(2).Returns(11);
            deviceRepositoryMock.Delete(device).Returns(Task<Domain.Device>.FromResult(device));
        }

        [Fact]
        public async void Get_Device_Returns_Element()
        {
            var service = new DeviceService(this.deviceRepositoryMock, mapper);

            var deviceResult = await service.Get(1);

            Assert.Null(deviceResult.Messages);
            Assert.True(deviceResult.Content==device);
        }

        [Fact]
        public async void Add_Device_Inserts_Element()
        {
            var service = new DeviceService(this.deviceRepositoryMock, mapper);

            var deviceResult = await service.AddDevice(deviceDto);

            Assert.Null(deviceResult.Messages);
        }
        [Fact]
        public async void Invalid_Device_Count_Throws_Error_On_Insert()
        {
            var tempDeviceDto = deviceDto;
            tempDeviceDto.GatewayId = 2;
            var service = new DeviceService(this.deviceRepositoryMock, mapper);
            var deviceResult = await service.AddDevice(tempDeviceDto);

            Assert.True(deviceResult.Messages!=null && deviceResult.Messages.Contains("The gateway has reached the devices limit. Please delete a device in order to add a new one"));
        }
        [Fact]
        public async void Invalid_UId_Throws_Error_On_Insert()
        {
            var tempDeviceDto = deviceDto;
            tempDeviceDto.UId = 0;
            var service = new DeviceService(this.deviceRepositoryMock, mapper);
            var deviceResult = await service.AddDevice(tempDeviceDto);

            Assert.True(deviceResult.Messages != null && deviceResult.Messages.Contains("Please, specify a universal id."));
        }
        [Fact]
        public async void Invalid_Vendor_Throws_Error_On_Insert()
        {
            var tempDeviceDto = deviceDto;
            tempDeviceDto.Vendor = "";
            var service = new DeviceService(this.deviceRepositoryMock, mapper);
            var deviceResult = await service.AddDevice(tempDeviceDto);

            Assert.True(deviceResult.Messages != null && deviceResult.Messages.Contains("Please, specify a vendor."));
        }
        [Fact]
        public async void Invalid_Id_Throws_Error_On_Delete()
        {
           
            var service = new DeviceService(deviceRepositoryMock, mapper);
            var gatewayResult = await service.Delete(0);

            Assert.True(gatewayResult.Messages != null && gatewayResult.Messages.Contains("Element to delete not found"));
        }

    }
}
