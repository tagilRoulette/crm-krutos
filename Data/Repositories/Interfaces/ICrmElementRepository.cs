using Crm.Data.Entities;

namespace Crm.Data.Repositories.Interfaces;

public interface ICrmElementsRepository
{
    Task<CrmElementEntity?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task AddAsync(CrmElementEntity element, CancellationToken cancellationToken);
    void Update(CrmElementEntity element);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}