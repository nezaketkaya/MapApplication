using MapApplication.Context;
using MapApplication.Data.Abstract;
using MapApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MapApplication.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        public Response<bool> Commit()
        {
            try
            {
                int changes = _context.SaveChanges();
                return Response<bool>.SuccessResponse(true, $"{changes} changes saved successfully.", 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.ErrorResponse($"An error occurred while saving changes: {ex.Message}", 500);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);

            /* GC.SuppressFinalize(this) metodu, 
             * .NET'in çöp toplayıcısına (Garbage Collector) bu nesneyi finalize etmesine gerek olmadığını söyler. 
            Eğer finalize metodu (yani, ~ClassName()) tanımlanmışsa, bu metot çağrıldığında sınıfın sonlandırıcısının çağrılmasını engeller.
            Finalize işlemi, çöp toplayıcı tarafından yapılan bir işlemdir ve sınıfın sonlandırıcısı varsa bu işlem biraz maliyetli olabilir. 
            Eğer Dispose metodu çağrıldıysa, finalize işlemine gerek kalmaz, GC.SuppressFinalize kullanılarak performans iyileştirilir.*/
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.TryGetValue(type, out var repositoryInstance))
            {
                repositoryInstance = new GenericRepository<T>(_context);
                _repositories[type] = repositoryInstance;
            }
            return (IGenericRepository<T>)repositoryInstance;
        }


        public void Rollback()
        {
            throw new NotImplementedException("Rollback is not implemented. Use transactions if necessary.");

            /* Rollback işlemi tanımlamak için DbContext sınıfının Transaction özelliğini kullanılabilir.
            Ancak, SaveChanges yapılmadan önce bir hata oluşursa zaten veritabanında herhangi bir değişiklik yapılmamış olur.
            Bu metodu NotImplementedException atacak şekilde bırakabiliriz, çünkü DbContext doğrudan rollback işlemi sunmaz. 
            Eğer daha karmaşık bir transaction yönetimi gerekiyorsa,
            DbContext.Database.BeginTransaction() ile manuel olarak transaction yönetimi yapılabilir. */
        }
    }
}
