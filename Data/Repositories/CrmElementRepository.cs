
using Crm.Data.Contexts;
using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Repositories;

public class CrmElementsRepository : ICrmElementsRepository
{
    private readonly AppDbContext _context;

    public CrmElementsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CrmElement?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Elements.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddAsync(CrmElement element, CancellationToken cancellationToken)
    {
        await _context.Elements.AddAsync(element, cancellationToken);
    }

    public void Update(CrmElement element)
    {
        _context.Elements.Update(element);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var element = await _context.Elements.FindAsync(new object[] { id }, cancellationToken);
        if (element != null)
        {
            _context.Elements.Remove(element);
        }
    }
}