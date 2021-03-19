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
    public  class GatewayService : BaseService<Domain.Gateway>, IGatewayService
    {
        public GatewayService(IGatewayRepository repository, IMapper mapper):base(repository, mapper)
        {
           
        }
      

    }
}

