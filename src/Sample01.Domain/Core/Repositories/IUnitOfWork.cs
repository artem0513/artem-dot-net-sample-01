using Sample01.Domain.Core.Models;

namespace Sample01.Domain.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task RollBackChangesAsync();
        IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
    }
}