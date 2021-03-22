using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class GatewayRepository:BaseRepository<Gateway>,IGatewayRepository
    {
        public GatewayRepository(TestDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Gateway> Get(int id) 
        {
  
           return  await this._entities.Include(x=>x.Devices).SingleOrDefaultAsync(s => s.Id == id);
           
        }
    }
}
