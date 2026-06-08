using System.Xml.Linq;
using Crm.Data.Repositories.Interfaces;
using Crm.Infrastructure;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;
using Microsoft.Extensions.FileProviders;
using static Crm.Infrastructure.Constants;

namespace Crm.Logic.Services;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IElementsService _elementsService;
    private readonly IPagesService _pagesService;
    private readonly EmbeddedFileProvider _fileProvider;

    public ProjectsService(
        IProjectsRepository projectsRepository,
        EmbeddedFileProvider fileProvider,
        IElementsService elementsService,
        IPagesService pagesService)
    {
        _projectsRepository = projectsRepository;
        _fileProvider = fileProvider;
        _elementsService = elementsService;
        _pagesService = pagesService;
    }

    public async Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken)
    {
        await _projectsRepository.ChangeProjectNameAsync(id, newName, cancellationToken);
    }

    public async Task<ProjectModel> CreateTemplateProjectAsync(CancellationToken cancellationToken)
    {
        string projectName = "Template project";

        var pageNames = EmbeddedResourcesReader.ReadJson<IReadOnlyList<string>>(PagesTemplateFileName, _fileProvider)
            ?? throw new FormatException("Elements template JSON is empty or invalid.");
        if (pageNames.Count == 0) throw new FormatException("Pages template names file is empty.");
        var elements = EmbeddedResourcesReader.ReadJson<IReadOnlyList<string>>(ElementsTemplateFileName, _fileProvider)
            ?? throw new FormatException("Elements template JSON is empty or invalid.");

        var projectGuid = Guid.NewGuid();
        var navType = NavigationType.Side;
        var createdAt = DateTime.UtcNow;

        var project = await _projectsRepository.CreateProjectAsync(
            projectGuid,
            projectName,
            navType,
            createdAt,
            cancellationToken);
        var pages = await _pagesService.CreatePagesAsync(projectGuid, pageNames, cancellationToken);
        IEnumerable<Guid> pageIds = pages.Select(z => z.Id);
        await _elementsService.CreateElementsAsync(pageIds, elements, cancellationToken);

        return new(projectGuid, projectName, navType, createdAt);
    }

    public async Task<ProjectModel> CreateProjectAsync(string name, NavigationType navType, CancellationToken cancellationToken)
    {
        var entity = await _projectsRepository.CreateProjectAsync(
            id: Guid.NewGuid(),
            name,
            navType,
            CreatedAt: DateTime.UtcNow,
            cancellationToken
            );
        return new(entity.Id, name, navType, entity.CreatedAt);
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
        return [..projects
            .Select(p => new ProjectModel(p.Id, p.Name ?? string.Empty, p.NavigationType, p.CreatedAt))
            ];
    }

    public async Task<ProjectModel> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectByIdAsync(id, cancellationToken);
        return new(project.Id, project.Name ?? string.Empty, project.NavigationType, project.CreatedAt);
    }
}
