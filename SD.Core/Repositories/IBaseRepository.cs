﻿using System.Linq.Expressions;
using Wifi.SD.Core.Entities;

namespace Wifi.SD.Core.Repositories
{
    public interface IBaseRepository // 8.
    {
        #region [C]REATE

        void Add<T>(T entity, bool saveImmediately = false)
            where T : class, IEntity;

        Task AddAsync<T>(T entity, bool saveImmediately = false, CancellationToken cancellation = default)
            where T : class, IEntity;

        #endregion

        #region [R]EAD

        IQueryable<T> QueryFrom<T>(Expression<Func<T, bool>> whereFilter = null)
            where T : class, IEntity;

        #endregion

        #region [U]PDATE

        T Update<T>(T entity, object key, bool saveImmediately = false)
            where T : class, IEntity;

        Task<T> UpdateAsync<T>(T entity, object key, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity;

        #endregion

        #region [D]ELETE

        void Remove<T>(T entity, bool saveImmediately = false)
            where T : class, IEntity;

        Task RemoveAsync<T>(T entity, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity;

        void Remove<T>(object key, bool saveImmediately = false)
            where T : class, IEntity;

        Task RemoveAsync<T>(object key, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity;

        #endregion
    }
}
