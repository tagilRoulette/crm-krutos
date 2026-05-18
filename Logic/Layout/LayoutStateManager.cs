using System.Collections.Concurrent;

namespace Crm.Logic.Layout;

public class LayoutStateManager
{
    private readonly ConcurrentDictionary<Guid, ElementPosition> _positions = new();

    public void UpdatePosition(Guid objectId, int x, int y)
    {
        _positions.AddOrUpdate(
            objectId,
            new ElementPosition { X = x, Y = y },
            (key, existing) =>
            {
                existing.X = x;
                existing.Y = y;
                return existing;
            });
    }

    public ElementPosition? GetElementState(Guid objectId)
    {
        _positions.TryGetValue(objectId, out var state);
        return state;
    }
}
