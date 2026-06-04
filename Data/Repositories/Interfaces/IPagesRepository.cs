using Data.Entities;

namespace Crm.Data.Repositories.Interfaces;

public interface IPagesRepository
{
    Task<IReadOnlyCollection<PageEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<PageEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PageEntity> CreateAsync(Guid pageId, Guid projectId, string name, DateTime createdAt, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<PageEntity>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
    Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
