using Crm.Data.Entities;
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
        var elements = EmbeddedResourcesReader.ReadJson<List<CrmElementEntity>>(projectName, _fileProvider);
        if (elements is null) throw new FileNotFoundException("Template JSON is empty or incorrect.");
        var project = await _projectsRepository.CreateProjectAsync(
            guid,
            projectName,
            navType,
            createdAt,
            elements,
            cancellationToken);
        var modelElements = elements
            .Select(z => new ElementModel()
            {
                Id = z.Id,
                Json = z.Json,
                LastModified = z.LastModified
            })
            .ToList();
        return new(guid, projectName, navType, createdAt, modelElements);
    }

    public async Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        await _projectsRepository.DeleteAllProjectsAsync(cancellationToken);
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        await _projectsRepository.DeleteProjectAsync(id, cancellationToken);
        return true;
    }

    public async Task<IReadOnlyCollection<ProjectModel>> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        var projects = await _projectsRepository.GetAllProjectsAsync(cancellationToken);
        return projects
            .Select(p => new ProjectModel(
                p.Id,
                p.Name,
                p.NavigationType,
                p.CreatedAt,
                p.Elements.Select(z =>
                new ElementModel()
                {
                    Id = z.Id,
                    Json = z.Json,
                    LastModified = z.LastModified
                })
            .ToList()))
            .ToArray();
    }

    public async Task<ProjectModel> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectByIdAsync(id, cancellationToken);
        var elements = project.Elements
            .Select(z => new ElementModel()
            {
                Id = z.Id,
                Json = z.Json,
                LastModified = z.LastModified
            })
            .ToList();
        return new(project.Id, project.Name, project.NavigationType, project.CreatedAt, elements);
    }
}
