using Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Gateway.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMusalaWebHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private IGatewayService _gatewayService;
        private IDeviceService _deviceService;
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(ILogger<GatewayController> logger, IGatewayService gatewayService, IDeviceService deviceService)
        {
            _logger = logger;
            _gatewayService = gatewayService;
            _deviceService = deviceService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GatewayDetailsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Result<GatewayDetailsDto>> Get(int id)
        {
            return await _gatewayService.GetById(id);
   
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<PaginationResultDto<GatewayDto>>> GetGateways(
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize=10,
            [FromQuery] string filterBy="",
            [FromQuery] string searchTerm="",
            [FromQuery] string sortBy="")
        {
            var pageInfo = new PaginationInfoDto(pageNumber, pageSize);
            string sortProperty;
            string sortDirection;
            switch (sortBy)
            {
                case "name":
                case "name_asc":
                    sortProperty = nameof(GatewayDto.Name);
                    sortDirection = "asc";
                    break;
                case "name_desc":
                    sortProperty = nameof(GatewayDto.Name);
                    sortDirection = "desc";
                    break;
                case "serialNumber":
                case "serialNumber_asc":
                    sortProperty = nameof(GatewayDto.SerialNumber);
                    sortDirection = "asc";
                    break;
                case "serialNumber_desc":
                    sortProperty = nameof(GatewayDto.SerialNumber);
                    sortDirection = "desc";
                    break;
                case "iPV4Address":
                case "iPV4Address_asc":
                    sortProperty = nameof(GatewayDto.IPV4Address);
                    sortDirection = "asc";
                    break;
                case "iPV4Address_desc":
                    sortProperty = nameof(GatewayDto.IPV4Address);
                    sortDirection = "desc";
                    break;
                default:
                    sortProperty = nameof(GatewayDto.Id);
                    sortDirection = "asc";
                    break;
            }

            return await _gatewayService.List(pageInfo, filterBy, searchTerm, sortProperty, sortDirection);
        }

 

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Result> Post(GatewayDto input)
        {

            return await _gatewayService.AddGateway(input);

        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete(int id)
        {

            return await _gatewayService.Delete(id);
        }

        [HttpPost("AddDevice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Result> AddDevice(DeviceDto input)
        {

            return await _deviceService.AddDevice(input);

        }
        [HttpDelete("DeleteDevice/{id}")]
        public async Task<Result> DeleteDevice(int id)
        {

            return await _deviceService.Delete(id);
        }


    }
}
