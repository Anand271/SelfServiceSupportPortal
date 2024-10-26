using System.Linq.Expressions;

namespace AadharVerify.Data.Core.DataRepository.RepositoryBase
{
    public interface IDataRepositoryBase<T>
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAsQueryable();
        void Create(T model);
        void Update(T model);
        void Delete(T model);
        T Find(long id);
        T Find(int id);

        T Find(string id);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FinddataByCondition(Expression<Func<T, bool>> expression);
        int CountAll<T>() where T : class;
    }
}
