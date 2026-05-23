using Crm.Data.Contexts;
using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Logic;

namespace Crm.Data.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private ProjectsDbContext _context;

    public ProjectsRepository(ProjectsDbContext context)
    {
        _context = context;
    }

    public async Task AddElementAsync(CrmElementEntity element, Guid projectId, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(projectId) 
            ?? throw new KeyNotFoundException($"Primary key {projectId} not found.");
        project.Elements.Add(element);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectEntity> CreateProjectAsync(
        Guid id,
        string projectName,
        NavigationType navigationType,
        DateTime CreatedAt,
        List<CrmElementEntity> LayoutJson,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ProjectEntity>> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectEntity> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectEntity> GetProjectTemplateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveElementAsync(Guid elementId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateElementAsync(Guid elementId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
