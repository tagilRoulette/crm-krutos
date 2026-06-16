using Crm.Logic.Models;

namespace Crm.Logic.Services.Interfaces
{
    public interface IPagesService
    {
        public Task<PageModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<PageModel>?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<PageModel>> GetAllAsync(CancellationToken cancellationToken);
        public Task<PageModel> CreateAsync(Guid projectId, string name, CancellationToken cancellationToken);
        public Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken);
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task DeleteAllAsync(CancellationToken cancellationToken);
        public Task<IReadOnlyCollection<PageModel>> CreatePagesAsync(Guid projectId, IReadOnlyList<string> names, CancellationToken cancellationToken);
    }
}
