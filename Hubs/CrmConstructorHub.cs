using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class CrmConstructorHub : Hub
{
    private readonly AppDbContext _context;
    public CrmConstructorHub(AppDbContext context)
    {
        _context = context;
    }
    public async Task MoveObject(string objectId, int x, int y)
    {
        var element = await _context.Elements.FindAsync(objectId);
        if (element != null)
        {
            element.X = x;
            element.Y = y;
            
        }
        else
        {
            element = new CrmElement { Id = objectId, X = x, Y = y };
            _context.Elements.Add(element);
            
        }
        element.LastModified = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await Clients.Others.SendAsync("ReceiveNewPosition", objectId, x, y);
    }
}