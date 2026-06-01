using Crm.Data.Contexts;
using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Repositories;

public class ElementsRepository : IElementsRepository
{
    private readonly ElementsDbContext _context;

    public ElementsRepository(ElementsDbContext context)
    {
        _context = context;
    }

    public async Task<ElementEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Elements.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyCollection<ElementEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Elements.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ElementEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Elements
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ElementEntity element, CancellationToken cancellationToken)
    {
        await _context.Elements.AddAsync(element, cancellationToken);
    }

    public void Update(ElementEntity element)
    {
        _context.Elements.Update(element);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var element = await GetByIdAsync(id, cancellationToken);
        if (element != null)
        {
            _context.Elements.Remove(element);
        }
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {

        await _context.Elements.ExecuteDeleteAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ElementEntity>> GetByPageIdAsync(Guid pageId, CancellationToken cancellationToken)
    {
        return await _context.Elements
            .Where(z => z.PageId == pageId)
            .ToListAsync(cancellationToken);
    }
}