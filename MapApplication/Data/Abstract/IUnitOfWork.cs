using MapApplication.Model;

namespace MapApplication.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Response<bool> Commit();
        void Rollback();
    }
}
