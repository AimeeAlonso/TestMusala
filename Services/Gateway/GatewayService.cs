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

namespace Services
{
    public  class GatewayService : BaseService<Domain.Gateway>, IGatewayService
    {
        public GatewayService(IGatewayRepository repository, IMapper mapper):base(repository, mapper)
        {
           
        }
        public async Task<Result<GatewayDetailsDto>> GetById(int gatewayId) 
        {
            var gatewayResult = await _repository.Get(gatewayId);
            return _mapper.Map<Result<GatewayDetailsDto>>(gatewayResult);
        }

        public async Task<Result> AddGateway(GatewayDto gateway)
        {
            Result result = new Result();
          
            try
            {
                await this._repository.Insert(_mapper.Map<Domain.Gateway>(gateway));
            }
            catch (System.Exception)
            {

                result.AddError("Unexpected error adding a device");
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

            var pageData = await gateways.Paginate(pageInfo).ToListAsync();

            var pageDataDto = _mapper.Map<IEnumerable<GatewayDto>>(pageData);

            return new PaginationResultDto<GatewayDto>(pageInfo, pageDataDto);
        }

    }
}

