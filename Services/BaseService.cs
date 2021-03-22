using AutoMapper;
using DataAccess.Repositories;
using Domain;
using Services.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity, new()
    {
        protected IRepository<T> _repository;
        protected string _defaultExceptionText;
        protected readonly IMapper _mapper;
        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
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
        public virtual async Task<Result> Delete(int id)
        {

            var result = new Result();

            try
            {
                var instance = await this._repository.Get(id);
                if (instance==null)
                {
                    result.AddError("Element to delete not found");
                }
                else  await this._repository.Delete(instance);
            }
            catch (System.Exception)
            {

                result.AddError("Unexpected error deleting ");
            }

            return result;

        }


        /// <summary>
        /// Base method for insert an object to repository.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<Result> Insert(T instance)
        {
            var result = new Result();
            if (instance == null)
            {
               
                result.AddError("Content for add is not filled.");

               
            }
            else
            {
                 try
                {
                    await this._repository.Insert(instance);
                }
                catch (System.Exception)
                {

                    result.AddError("Unexpected error inserting ");
                }

            }
            return result;

        }


        /// <summary>
        /// Base method for update an object from repository.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<Result> Update(T instance)
        {
            var result = new Result();

            if (instance == null)
            {
                result.AddError("Content for update is not filled.");

            }

            else if (instance.Id == 0)
            {
                result.AddError("Record not found (Id is zero).");

            }
            else
            {
                try
                {
                    await this._repository.Update(instance);
                }
                catch (System.Exception)
                {

                    result.AddError("Unexpected error updating ");
                }
            }
           
            return result;

        }



        /// <summary>
        /// Base method for getting all objects from repository.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return  this._repository.GetAll();
        }

        /// <summary>
        /// /// Base method for getting an object from repository (async).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<T>> Get(int id)
        {
            var result = new Result<T>();

             if (id == 0)
            {
                result.AddError("Record not found (Id is zero).");

            }
            else
            {
                try
                {
                    result.Content = await this._repository.Get(id);
                }
                catch (System.Exception)
                {

                    result.AddError("Unexpected error finding element ");
                }
            }
            return result;
        }

    }
}

