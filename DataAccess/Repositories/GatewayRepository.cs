using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
   public class GatewayRepository:BaseRepository<Gateway>,IGatewayRepository
    {
        public GatewayRepository(IConfiguration config) : base(config)
        {
        }
    }
}
