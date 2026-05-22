using Crm.Logic.Models;

namespace Crm.Logic.Services.Interfaces
{
    public interface IElementsService
    {
        public Task<ElementModel> GetElementByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<ElementModel>> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<ElementModel>> GetAllElementsAsync(CancellationToken cancellationToken);
        public Task<ElementModel> CreateElementAsync(string? json, CancellationToken cancellationToken);
        public Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken);
        public Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken);
        public Task DeleteAllElementsAsync(CancellationToken cancellationToken);
    }
}
