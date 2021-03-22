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
   
    public class GatewayServiceTests
    {
        IGatewayRepository gatewayRepositoryMock = Substitute.For<IGatewayRepository>();
        IMapper mapper;
        public GatewayServiceTests()
        {
            ConfigureData();
        }
        private Domain.Gateway gateway;
        private GatewayDto gatewayDto;
        private GatewayDetailsDto gatewayDetailsDto;
        private void ConfigureData() 
        {
             gateway = new Domain.Gateway()
            {
                Id = 1,
                SerialNumber = "G1",
                IPV4Address = "12.12.1.2",
                Name = "G1"

            };
            gatewayDto = new GatewayDto()
            {
                Id = 1,
                SerialNumber = "G1",
                IPV4Address = "12.12.1.2",
                Name = "G1"

            };
            gatewayDetailsDto = new GatewayDetailsDto()
            {
                Id = 1,
                SerialNumber = "G1",
                IPV4Address = "12.12.1.2",
                Name = "G1",
                Devices = new List<DeviceDto>()

            };
            mapper = new MapperConfiguration(c => c.AddProfile<AutoMapping>()).CreateMapper();
            gatewayRepositoryMock.Get(gateway.Id).Returns(Task<Domain.Gateway>.FromResult(gateway));
            gatewayRepositoryMock.Insert(gateway).Returns(Task<Domain.Gateway>.FromResult(gateway));
            gatewayRepositoryMock.Delete(gateway).Returns(Task<Domain.Gateway>.FromResult(gateway));
        }

        bool CompareGatewayDetailsDtos(GatewayDetailsDto a, GatewayDetailsDto b) 
        {
            bool equalDevices =true;
            var list1 = a.Devices.ToList();
            var list2 = b.Devices.ToList();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i]!=list2[i])
                {
                    equalDevices = false;
                    break;
                }
            } 
            return a.Id == b.Id && a.Name == b.Name && a.IPV4Address == b.IPV4Address && list1.Count == list2.Count&& equalDevices;
        }
        [Fact]
        public async void Get_Gateway_Returns_Element()
        {
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);

            var gatewayResult = await service.GetById(1);

            Assert.Null(gatewayResult.Messages);
            Assert.True(CompareGatewayDetailsDtos(gatewayResult.Content,gatewayDetailsDto));
        }

        [Fact]
        public async void Insert_Gateway_Inserts_Element()
        {
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);

            var gatewayResult = await service.AddGateway(gatewayDto);

            Assert.Null(gatewayResult.Messages);
        }
        [Fact]
        public async void Invalid_Ip_Address_Throws_Error_On_Insert()
        {
            var tempGatewayDto = gatewayDto;
            tempGatewayDto.IPV4Address = "asd";
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);
            var gatewayResult = await service.AddGateway(tempGatewayDto);

            Assert.True(gatewayResult.Messages!=null && gatewayResult.Messages.Contains("Invalid IPV4 address"));
        }
        [Fact]
        public async void Invalid_Name_Throws_Error_On_Insert()
        {
            var tempGatewayDto = gatewayDto;
            tempGatewayDto.Name="";
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);
            var gatewayResult = await service.AddGateway(gatewayDto);

            Assert.True(gatewayResult.Messages != null && gatewayResult.Messages.Contains("Please, specify a name."));
        }
        [Fact]
        public async void Invalid_Serial_Number_Throws_Error_On_Insert()
        {
            var tempGatewayDto = gatewayDto;
            tempGatewayDto.SerialNumber = "";
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);
            var gatewayResult = await service.AddGateway(gatewayDto);

            Assert.True(gatewayResult.Messages != null && gatewayResult.Messages.Contains("Please, specify a serial number."));
        }
        [Fact]
        public async void Invalid_Id_Throws_Error_On_Delete()
        {
           
            var service = new GatewayService(this.gatewayRepositoryMock, mapper);
            var gatewayResult = await service.Delete(0);

            Assert.True(gatewayResult.Messages != null && gatewayResult.Messages.Contains("Element to delete not found"));
        }

    }
}
