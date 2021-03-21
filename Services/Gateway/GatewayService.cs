using AutoMapper;
using DataAccess.Repositories;
using Domain;
using Services.Utils;
using Services.Gateway.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Services
{
    public  class GatewayService : BaseService<Domain.Gateway>, IGatewayService
    {
        public GatewayService(IGatewayRepository repository, IMapper mapper):base(repository, mapper)
        {
           
        }
        public async Task<Result<GatewayDetailsDto>> GetById(int gatewayId) 
        {
            Result< GatewayDetailsDto> result = new Result<GatewayDetailsDto>();
            try
            {
                var gateway = await _repository.Get(gatewayId);
                var gatewayResult = _mapper.Map<GatewayDetailsDto>(gateway);
                result.Content = gatewayResult;
            }
            catch (Exception)
            {

                result.AddError("Unexpected error retrieving gateway");
            }

            return result;
        }

        public async Task<Result> AddGateway(GatewayDto gateway)
        {
            Result result = new Result();
            IPAddress ipv4Address = null;
            IPAddress.TryParse(gateway.IPV4Address, out ipv4Address);
            if (ipv4Address==null)
            {
                result.AddError("Invalid IPV4 address");
            }
            else
            {
                try
                {
                    await this._repository.Insert(_mapper.Map<Domain.Gateway>(gateway));
                }
                catch (System.Exception)
                {

                    result.AddError("Unexpected error adding a device");
                }
            }
            return result;
        }
        public async Task<PaginationResultDto<GatewayDto>> List(PaginationInfoDto pageInfo,
         string filterBy, string filterTerm, string sortBy, string sortDirection)
        {
            var gateways =  _repository.GetAll();

            if (!String.IsNullOrEmpty(filterBy) && !String.IsNullOrEmpty(filterTerm))
            {
                gateways = gateways.FilterBy(filterBy, filterTerm);
            }

            if (!String.IsNullOrEmpty(sortBy))
            {
                gateways = gateways.SortBy(sortBy, sortDirection);
            }
            else
            {
                gateways = gateways.SortBy("Id", "asc");
            }
            
            var pageData = gateways.Count()>0 ?  await gateways.Paginate(pageInfo).ToListAsync():new List<Domain.Gateway>();

            //var pageDataDto = _mapper.Map<IEnumerable<GatewayDto>>(pageData);
            var pageDataDto = pageData.Select(gw => _mapper.Map<GatewayDto>(gw));

            return new PaginationResultDto<GatewayDto>(pageInfo, pageDataDto);
        }

    }
}

