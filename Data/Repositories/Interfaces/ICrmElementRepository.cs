using Crm.Data.Entities;

namespace Crm.Data.Repositories.Interfaces;

public interface ICrmElementRepository
{
    Task<CrmElementEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CrmElementEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CrmElementEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task AddAsync(CrmElementEntity element, CancellationToken cancellationToken);
    void Update(CrmElementEntity element);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
