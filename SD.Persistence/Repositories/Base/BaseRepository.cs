using SD.Persistence.Repositories.DBContext;
using System.Linq.Expressions;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Repositories;

namespace SD.Persistence.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        MovieDbContext movieDbContext;

        public BaseRepository()
        {
            this.movieDbContext = new();
        }

        public BaseRepository(MovieDbContext movieDbContext)
        {
            this.movieDbContext = new();
        }

        #region [C]REATE
        public void Add<T>(T entity, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Add(entity);

            if (saveImmediately) { this.movieDbContext.SaveChanges(); }

        }

        public async Task AddAsync<T>(T entity, bool saveImmediately = false, CancellationToken cancellation = default)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Add(entity);

            if (saveImmediately) { await this.movieDbContext.SaveChangesAsync(cancellation); }
        }
        #endregion

        #region [R]EAD
        public IQueryable QueryFrom<T>(Expression<Func<T, bool>> whereFilter)
            where T : class, IEntity
        {
            var query = this.movieDbContext.Set<T>();
            if (whereFilter != null)
            {
                return query.Where(whereFilter);
            }

            return query;
        }
        #endregion

        #region [U]PDATE

        public T Update<T>(T entity, object key, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = this.movieDbContext.Set<T>().Find(key);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    this.movieDbContext.SaveChanges();
                }
            }

            return toUpdate;
        }
        

        public async Task<T> UpdateAsync<T>(T entity, object key, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = await this.movieDbContext.Set<T>().FindAsync(key, cancellationToken);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    await this.movieDbContext.SaveChangesAsync();
                }
            }

            return toUpdate;
        }
        #endregion

        #region [D]ELETE
        public void Remove<T>(T entity, bool saveImmediately = false)
           

            where T : class, IEntity
        {
            if (entity == null) { return; }

            this.movieDbContext.Remove(entity);

            if(saveImmediately)
            {
                this.movieDbContext.SaveChanges(); 
            }
        }

        public async Task RemoveAsync<T>(T entity, bool saveImmediately = false, 
            CancellationToken cancellationToken = default)

            where T : class, IEntity
        {
            if (entity == null) { return; }

            this.movieDbContext.Remove(entity);

            if (saveImmediately)
            {
                await this.movieDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void Remove<T>(object key, bool saveImmediately)
            where T : class, IEntity
        {
            if (key == null) { return; }

            var toRemove = this.movieDbContext.Set<T>().Find(key);

            if (toRemove != null) 
            { 
                this.movieDbContext.Remove<T>(toRemove);

                if (saveImmediately)
                {
                    this.movieDbContext.SaveChanges();
                }
            }
        }

        public async Task RemoveAsync<T>(object key, bool saveImmediately = false, 
            CancellationToken cancellationToken = default)

        where T : class, IEntity
        {
            if (key == null) { return; }

            var toRemove = await this.movieDbContext.Set<T>().FindAsync(key);

            if (toRemove != null)
            {
                this.movieDbContext.Remove<T>(toRemove);

                if (saveImmediately)
                {
                    await this.movieDbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
        #endregion
    }
}
