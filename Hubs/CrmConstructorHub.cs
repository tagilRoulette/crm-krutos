using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class CrmConstructorHub : Hub
{
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
    private readonly LayoutStateManager _stateManager;
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public CrmConstructorHub(LayoutStateManager stateManager, IDbContextFactory<AppDbContext> dbFactory)
    {
        _stateManager = stateManager;
        _dbFactory = dbFactory;
    }

   
    public async Task MoveObject(string objectId, int x, int y)
    {
        _stateManager.UpdatePosition(objectId, x, y);

        // Мгновенно рассылаем всем остальным
        await Clients.Others.SendAsync("ReceiveNewPosition", objectId, x, y);
    }

    // 2. ФИКСАЦИЯ В БД - Вызывается, когда мышку отпустили (onDragStop)
    public async Task SaveElementPosition(string objectId)
    {
        var finalState = _stateManager.GetElementState(objectId);

        if (finalState != null)
        {
            // Открываем короткое соединение с базой
            using var context = await _dbFactory.CreateDbContextAsync();

            var element = await context.Elements.FindAsync(objectId);
            if (element != null)
            {
                element.X = finalState.X;
                element.Y = finalState.Y;
            }
            else
            {
                element = new CrmElement { Id = objectId, X = finalState.X, Y = finalState.Y };
                context.Elements.Add(element);
            }

            element.LastModified = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
<<<<<<< HEAD
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
}