﻿using Domain;
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
        Result Insert(T instance);

        Result Insert(IEnumerable<T> instances);

        Result Update(T instance);

        Result Update(IEnumerable<T> instances);

        Result Delete(T instance);

        Result Delete(IEnumerable<T> instances);

        Result<IEnumerable<T>> Get();

        Task<Result<T>> Get(int id);

        Result<IEnumerable<T>> Get(IEnumerable<int> ids);
    }
}
