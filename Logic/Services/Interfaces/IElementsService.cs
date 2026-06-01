using Crm.Logic.Models;

namespace Crm.Logic.Services.Interfaces
{
    public interface IElementsService
    {
        public Task<ElementModel> GetElementByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<ElementModel>> GetElementsByPageIdAsync(Guid projectId, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<ElementModel>> GetAllElementsAsync(CancellationToken cancellationToken);
        public Task<ElementModel> CreateElementAsync(Guid pageId, string? json, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<ElementModel>> CreateElementsAsync(IEnumerable<Guid> pageIds, IReadOnlyList<string?> jsons, CancellationToken cancellationToken);
        public Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken);
        public Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken);
        public Task DeleteAllElementsAsync(CancellationToken cancellationToken);
    }
}
