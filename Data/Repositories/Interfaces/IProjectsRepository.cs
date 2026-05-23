using Crm.Data.Entities;
using Crm.Logic;

namespace Crm.Data.Repositories.Interfaces;

public interface IProjectsRepository
{
    public Task<ProjectEntity> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<ProjectEntity>> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectEntity> CreateProjectAsync(
        Guid id,
        string projectName,
        NavigationType navigationType,
        DateTime CreatedAt,
        List<CrmElementEntity> LayoutJson,
        CancellationToken cancellationToken);
    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken);
    public Task AddElementAsync(CrmElementEntity element, Guid projectId, CancellationToken cancellationToken);
    public Task UpdateElementAsync(Guid elementId, CancellationToken cancellationToken);
    public Task RemoveElementAsync(Guid elementId, CancellationToken cancellationToken);
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectEntity> GetProjectTemplateAsync(CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
