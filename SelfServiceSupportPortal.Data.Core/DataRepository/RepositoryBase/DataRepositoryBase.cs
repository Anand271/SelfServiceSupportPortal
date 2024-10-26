

using Microsoft.EntityFrameworkCore;
using SelfServiceSupportPortal.Data.Core.Data;
using System.Linq.Expressions;

namespace AadharVerify.Data.Core.DataRepository.RepositoryBase
{
    public class DataRepositoryBase<T> : IDataRepositoryBase<T> where T : class
    {
        private readonly SelfServiceSupportPortalDbContext selfServiceSupportPortalDbContext;
        public DataRepositoryBase(SelfServiceSupportPortalDbContext appDbContext) 
        {
            selfServiceSupportPortalDbContext = appDbContext;
        }
        public IEnumerable<T> GetAll()
        {
            return selfServiceSupportPortalDbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAsQueryable()
        {
            return selfServiceSupportPortalDbContext.Set<T>();
        }
        public void Create(T model)
        {
            this.selfServiceSupportPortalDbContext.Set<T>().Add(model);
            this.selfServiceSupportPortalDbContext.SaveChanges();
        }

        public void Delete(T model)
        {
            this.selfServiceSupportPortalDbContext.Set<T>().Remove(model);
            this.selfServiceSupportPortalDbContext.SaveChanges();
        }

        public void Update(T model)
        {
            this.selfServiceSupportPortalDbContext.Set<T>().Update(model);
            this.selfServiceSupportPortalDbContext.SaveChanges();
        }

        public T Find(long id)
        {
            return this.selfServiceSupportPortalDbContext.Set<T>().Find(id);
        }

        public T Find(int id)
        {
            return this.selfServiceSupportPortalDbContext.Set<T>().Find(id);
        }

        public T Find(string id)
        {
            T result = this.selfServiceSupportPortalDbContext.Set<T>().Find(id);
            return result;
        }

        public int CountAll<T>() where T : class
        {
            return this.selfServiceSupportPortalDbContext.Set<T>().Count();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.selfServiceSupportPortalDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public T FinddataByCondition(Expression<Func<T, bool>> expression)
        {
            return this.selfServiceSupportPortalDbContext.Set<T>().Where(expression).FirstOrDefault();
        }
    }
}
