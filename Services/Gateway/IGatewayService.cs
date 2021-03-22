using Services.Utils;
using Services.Gateway.Dtos;
using System.Threading.Tasks;

namespace Services
{
    public interface IGatewayService: IService<Domain.Gateway>
    {
        Task<PaginationResultDto<GatewayDto>> List(PaginationInfoDto pageInfo,
        string filterBy, string filterTerm, string sortBy, string sortDirection);
        Task<Result<GatewayDetailsDto>> GetById(int gatewayId);

        Task<Result> AddGateway(GatewayDto gateway);
    }
}