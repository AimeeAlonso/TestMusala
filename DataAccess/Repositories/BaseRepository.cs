using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Abstract class of Repository pattern.
    /// In this layer, all repositories inherit the characteristics of this class.
    /// With this we guarantee the execution of an important pillar of OO and, consequently, we achieve an excellent code reduction.
    /// Note that other layers dont know that data is persisted with EF Core Code First (encapsulation pillar).
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        internal readonly TestDbContext _dbContext;
        protected readonly DbSet<T> _entities;
        protected string _defaultExceptionText;

        public BaseRepository(TestDbContext dbContext )
        {
            this._dbContext = dbContext;
            this._entities = this._dbContext.Set<T>();
            this._defaultExceptionText = "An unexpected error occurred while";
        }

        /// <summary>
        /// Base method for deleting an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task Delete(T instance)
        {
           
                this._entities.Remove(instance);
                await this._dbContext.SaveChangesAsync();
        }

     

        /// <summary>
        /// Base method for getting an object from database (async).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> Get(int id)
        {

             return await this._entities.SingleOrDefaultAsync(s => s.Id == id);
            
        }


        public virtual IQueryable<T> GetAll()
        {
            return  _entities.AsQueryable<T>();
        }

        /// <summary>
        /// Base method for insert an object to database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task Insert(T instance)
        {

                await this._entities.AddAsync(instance);
                await this._dbContext.SaveChangesAsync();
          
        }



        /// <summary>
        /// Base method for update an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task Update(T instance)
        { 
                if (instance != null)
                {
                    this._entities.Update(instance);
                   await this._dbContext.SaveChangesAsync();
                }
        }

    }
}
