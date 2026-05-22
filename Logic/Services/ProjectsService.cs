using Crm.Data.Repositories.Interfaces;
using Crm.Infrastructure;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;
using Microsoft.Extensions.FileProviders;

namespace Crm.Logic.Services;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly EmbeddedFileProvider _fileProvider;

    public ProjectsService(IProjectsRepository projectsRepository, EmbeddedFileProvider fileProvider)
    {
        _projectsRepository = projectsRepository;
        _fileProvider = fileProvider;
    }

    public async Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken)
    {
        await _projectsRepository.ChangeProjectNameAsync(id, newName, cancellationToken);
    }

    public async Task<ProjectModel> CreateProjectAsync(string projectName, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var navType = NavigationType.Side;
        var createdAt = DateTime.UtcNow;
        var elements = EmbeddedResourcesReader.ReadJson<List<Guid>>(projectName, _fileProvider);
        if (elements is null) throw new FileNotFoundException("Template JSON is empty or incorrect.");
        var project = await _projectsRepository.CreateProjectAsync(
            guid,
            projectName,
            navType,
            createdAt,
            elements,
            cancellationToken);
        return new(guid, projectName, navType, createdAt, elements);
    }

    public async Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        await _projectsRepository.DeleteAllProjectsAsync(cancellationToken);
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _projectsRepository.DeleteProjectAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProjectModel>> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        return await GetAllProjectsAsync(cancellationToken);
    }

    public async Task<ProjectModel> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await GetProjectByIdAsync(id, cancellationToken);
    }
}
