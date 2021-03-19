using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class GatewayRepository:BaseRepository<Gateway>,IGatewayRepository
    {
        public GatewayRepository(IConfiguration config) : base(config)
        {
        }

        public override async Task<Gateway> Get(int id) 
        {
  
           return  await this._entities.Include(x=>x.Devices).SingleOrDefaultAsync(s => s.Id == id);
           
        }
    }
}
