using Studly.Entities.Base;

namespace Studly.BLL.Interfaces.Services
{
    public interface IDatabaseService
    {
        Task<T> GetEntityAsync<T>(int id) where T : BaseEntity, new();
        Task<List<T>> GetEntitiesAsync<T>() where T : BaseEntity, new();
        Task<int> SaveEntityAsync<T>(T entity) where T : BaseEntity, new();
        Task<int> DeleteEntityAsync<T>(BaseEntity entity) where T : BaseEntity, new();
    }
}
