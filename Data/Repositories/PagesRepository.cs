using Crm.Data.Contexts;
using Crm.Data.Repositories.Interfaces;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Repositories;

public class PagesRepository : IPagesRepository
{
    private readonly PagesDbContext _context;

    public PagesRepository(PagesDbContext context)
    {
        _context = context;
    }

    public async Task<PageEntity> CreateAsync(Guid pageId, Guid projectId, string name, DateTime createdAt, CancellationToken cancellationToken)
    {
        PageEntity page = new()
        {
            Id = pageId,
            Name = name,
            ProjectId = projectId,
            CreatedAt = createdAt,
        };
        await _context.PageEntity.AddAsync(page, cancellationToken);
        return page;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _context.PageEntity.ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var page = await GetByIdAsync(id, cancellationToken);
        if (page is not null)
        {
            _context.PageEntity.Remove(page);
        }
    }

    public async Task<IReadOnlyCollection<PageEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.PageEntity
            .Include(z => z.Elements)
            .ToListAsync(cancellationToken);
    }

    public async Task<PageEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.PageEntity
            .Include(z => z.Elements)
            .FirstOrDefaultAsync(z => z.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PageEntity>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.PageEntity
            .Where(z => z.ProjectId == projectId)
            .Include(z => z.Elements)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken)
    {
        var page = await _context.FindAsync<PageEntity>([pageId], cancellationToken)
            ?? throw new KeyNotFoundException($"Page entity by id {pageId} is not found.");
        page.Name = newName;
        _context.Update(page);
    }
}
