using Crm.Data.Entities;
using Data.Entities;

namespace Crm.Data.Repositories.Interfaces;

public interface IElementsRepository
{
    Task<ElementEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ElementEntity>> GetAllAsync(CancellationToken cancellationToken);
    // TODO ѕрописать аналогичный метод всем репозитори€м?
    Task<IReadOnlyCollection<ElementEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task AddAsync(ElementEntity element, CancellationToken cancellationToken);
    void Update(ElementEntity element);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ElementEntity>> GetByPageIdAsync(Guid pageId, CancellationToken cancellationToken);
}
