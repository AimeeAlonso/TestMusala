using DataAccess.Repositories;
using Domain;
using Domain.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity, new()
    {
        protected IRepository<T> _repository;
        protected string _defaultExceptionText;

        public BaseService(IRepository<T> repository)
        {
            this._repository = repository;
            this._defaultExceptionText = "try again or contact the responsible team.";
        }

        /// <summary>
        /// Base method for geting a new instance of an object (low coupling).
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            return new T();
        }

        /// <summary>
        /// Base method for deleting a object from repository.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual Result Delete(T instance)
        {


            if (instance == null)
            {
                var result = new Result();
                result.AddError("Content for deletion is not filled.");

                return result;
            }

            return this._repository.Delete(instance);

        }

        /// <summary>
        /// Base method for deleting a object collection from repository.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public virtual Result Delete(IEnumerable<T> instances)
        {
            if (instances == null || instances.Count() == 0)
            {
                /*
                var result = new Result();
                result.AddError("Content for deletion is not filled.");

                return result;*/

                return new Result();
            }

            return this._repository.Delete(instances);
        }

        /// <summary>
        /// Base method for insert an object to repository.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual Result Insert(T instance)
        {

            if (instance == null)
            {
                var result = new Result();
                result.AddError("Content for add is not filled.");

                return result;
            }

            return this._repository.Insert(instance);

        }

        /// <summary>
        /// Base method for insert an object collection to repository.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public virtual Result Insert(IEnumerable<T> instances)
        {
            if (instances == null || instances.Count() == 0)
            {
                var result = new Result();
                result.AddError("Content for add is not filled.");

                return result;
            }

            return this._repository.Insert(instances);
        }

        /// <summary>
        /// Base method for update an object from repository.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual Result Update(T instance)
        {

            if (instance == null)
            {
                var result = new Result();
                result.AddError("Content for update is not filled.");

                return result;
            }

            if (instance.Id == 0)
            {
                var result = new Result();
                result.AddError("Record not found (Id is zero).");

                return result;
            }

            return this._repository.Update(instance);

        }

        /// <summary>
        /// Base method for update an object collection from repository.
        /// </summary>
        /// <param name="instances"></param>
        /// <returns></returns>
        public virtual Result Update(IEnumerable<T> instances)
        {
            if (instances == null || instances.Count() == 0)
            {
                var result = new Result();
                result.AddError("Content for update is not filled.");

                return result;
            }

            return this._repository.Update(instances);
        }

        /// <summary>
        /// Base method for getting all objects from repository.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<T>> Get()
        {
            return this._repository.Get();
        }

        /// <summary>
        /// /// Base method for getting an object from repository (async).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<T>> Get(int id)
        {
            return await this._repository.Get(id);
        }

        /// <summary>
        /// Base method for getting an object collection from repository filtering by related ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Result<IEnumerable<T>> Get(IEnumerable<int> ids)
        {

            if (ids == null || ids.Count() == 0)
                return new Result<IEnumerable<T>>()
                {
                    Content = new List<T>()
                };

            return this._repository.Get(ids);

        }
    }
}

