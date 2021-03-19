using Domain;
using Services.Utils;
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
    public interface IService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Ensure low coupling (avoid using 'new')
        /// </summary>
        /// <returns></returns>
        T New();

        /// <summary>
        /// Insert instance in database (instance is inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<Result> Insert(T instance);


        /// <summary>
        /// Update instance in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<Result> Update(T instance);


        /// <summary>
        /// Delete instance in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);


        /// <summary>
        /// Get all instances in database (instance inherit from 'BaseEntity').
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get instance in database by 'id' attribute (instance inherit from 'BaseEntity').
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<T>> Get(int id);

    }
}