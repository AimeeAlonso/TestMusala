using Domain;
using Services.Utils;
using Services.Gateway.Dtos;
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
    public interface IGatewayService: IService<Domain.Gateway>
    {
        Task<PaginationResultDto<GatewayDto>> List(PaginationInfoDto pageInfo,
        string filterBy, string filterTerm, string sortBy, string sortDirection);
        Task<Result<GatewayDetailsDto>> GetById(int gatewayId);

        Task<Result> AddGateway(GatewayDto gateway);
    }
}