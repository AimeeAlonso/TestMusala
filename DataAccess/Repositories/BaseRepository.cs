using Domain;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public BaseRepository( IConfiguration config)
        {
            string sqlStr = config.GetConnectionString("default");

            this._dbContext = new TestDbContext(sqlStr);
            this._entities = this._dbContext.Set<T>();
            this._defaultExceptionText = "An unexpected error occurred while";
        }

        /// <summary>
        /// Base method for deleting an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Delete(T instance)
        {
            var result = new Result();

            try
            {
                this._entities.Remove(instance);
                this._dbContext.SaveChanges();
            }
            catch (Exception)
            {

                result.AddError($"{this._defaultExceptionText} deleting record, " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for deleting a object collection from database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Delete(IEnumerable<T> instances)
        {
            var result = new Result();

            try
            {
                foreach (var instance in instances)
                {
                    this._entities.Remove(instance);
                }

                this._dbContext.SaveChanges();
            }
            catch (Exception )
            {

                result.AddError($"{this._defaultExceptionText} deleting records, " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for getting all objects from database.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<T>> Get()
        {
            var result = new Result<IEnumerable<T>>();

            try
            {
                result.Content = this._entities.AsEnumerable();
            }
            catch (Exception )
            {

                result.AddError($"{this._defaultExceptionText} getting record by 'Id', " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for getting an object from database (async).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<T>> Get(int id)
        {
            var result = new Result<T>();

            try
            {
                result.Content = await this._entities.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception )
            {

                result.AddError($"{this._defaultExceptionText} getting record by 'Id', " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for getting an object collection from database filtering by related ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual Result<IEnumerable<T>> Get(IEnumerable<int> ids)
        {
            var result = new Result<IEnumerable<T>>();

            try
            {
                result.Content = this._entities.Where(s => ids.Any(id => id == s.Id));
            }
            catch (Exception )
            {

                result.AddError($"{this._defaultExceptionText} getting record by 'Ids', " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for insert an object to database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Insert(T instance)
        {
            var result = new Result();

            try
            {
                this._entities.Add(instance);
                this._dbContext.SaveChanges();
            }
            catch (Exception )
            {

                result.AddError($"{this._defaultExceptionText} inserting record, " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for insert an object collection to database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Insert(IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Base method for update an object from database.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Result Update(T instance)
        {
            var result = new Result();

            try
            {
                if (instance != null)
                {
                    this._entities.Update(instance);
                    this._dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                result.AddError($"{this._defaultExceptionText} updating record, " +
                    $"try again or request technical team to view logs etc.");
            }

            return result;
        }

        /// <summary>
        /// Base method for update an object collection from database.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public Result Update(IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }
    }
}
