using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Base contract of Repository pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task Insert(T instance);


        Task Update(T instance);


        Task Delete(T instance);


        Task<T> Get(int id);

        IQueryable<T> GetAll();
    }
}
