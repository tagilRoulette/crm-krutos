using Crm.Data.Entities;

namespace Crm.Data.Repositories.Interfaces;

public interface ICrmElementsRepository
{
    Task<CrmElement?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task AddAsync(CrmElement element, CancellationToken cancellationToken);
    void Update(CrmElement element);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}