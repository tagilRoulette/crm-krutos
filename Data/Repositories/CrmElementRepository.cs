using Crm.Data.Contexts;
using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Repositories;

public class CrmElementRepository : ICrmElementRepository
{
    private readonly ElementsDbContext _context;

    public CrmElementRepository(ElementsDbContext context)
    {
        _context = context;
    }

    public async Task<CrmElementEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Elements.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyCollection<CrmElementEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Elements.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CrmElementEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Elements
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CrmElementEntity element, CancellationToken cancellationToken)
    {
        await _context.Elements.AddAsync(element, cancellationToken);
    }

    public void Update(CrmElementEntity element)
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
}