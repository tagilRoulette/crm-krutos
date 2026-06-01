using Crm.Data.Contexts;
using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Logic;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private ProjectsDbContext _context;

    public ProjectsRepository(ProjectsDbContext context)
    {
        _context = context;
    }

    public async Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(id, cancellationToken);
        if (project is null) throw new KeyNotFoundException($"Project by id {id} not found.");
        project.Name = newName;
        _context.Projects.Update(project);
    }

    public async Task<ProjectEntity> CreateProjectAsync(
        Guid id,
        string projectName,
        NavigationType navigationType,
        DateTime createdAt,
        CancellationToken cancellationToken)
    {
        ProjectEntity project = new()
        {
            Id = id,
            Name = projectName,
            CreatedAt = createdAt,
            NavigationType = navigationType
        };
        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return project;
    }

    public async Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        await _context.Projects.ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await GetProjectByIdAsync(id, cancellationToken);
        if (project != null)
        {
            _context.Projects.Remove(project);
        }
    }

    public async Task<IReadOnlyCollection<ProjectEntity>> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<ProjectEntity> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Projects.FindAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Project by id {id} not found.");
    }

    public Task<ProjectEntity> GetProjectTemplateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
