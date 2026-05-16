using System.Collections.Concurrent;

namespace Crm.Layout;

public class LayoutStateManager
{
    private readonly ConcurrentDictionary<string, ElementPosition> _positions = new();

    public void UpdatePosition(string objectId, int x, int y)
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

    public ElementPosition? GetElementState(string objectId)
    {
        _positions.TryGetValue(objectId, out var state);
        return state;
    }
}
