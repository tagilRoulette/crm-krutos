using Crm.Logic.Models;

namespace Crm.Logic.Services.Interfaces;

public interface IProjectsService
{
    public Task<ProjectModel> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<ProjectModel>> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectModel> CreateProjectAsync(string projectName, NavigationType navType, CancellationToken cancellationToken);
    public Task<ProjectModel> CreateTemplateProjectAsync(CancellationToken cancellationToken);
    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken);
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken);
}
