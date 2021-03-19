using Domain;
using Domain.Utils;
using System;
using System.Collections.Generic;
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
        Task<Result> Insert(T instance);


        Task<Result> Update(T instance);


        Task<Result> Delete(T instance);

        Task<Result> Delete(IEnumerable<T> instances);

        Task<Result<IEnumerable<T>>> Get();

        Task<Result<T>> Get(int id);

        Task<Result<IEnumerable<T>>> Get(IEnumerable<int> ids);
    }
}
