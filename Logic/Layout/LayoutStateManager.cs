using System.Collections.Concurrent;

namespace Crm.Logic.Layout;

public class LayoutStateManager
{
    private readonly ConcurrentDictionary<Guid, string> _positions = new();

    public void AddOrUpdateState(Guid objectId, string json)
    {
        _positions.AddOrUpdate(
            objectId,
            json,
            (_, _) => json);
    }

    public void Remove(Guid objectId)
    {
        _positions.Remove(objectId, out var _);
    }

    public void RemoveAll()
    {
        _positions.Clear();
    }

    public string? GetElementState(Guid objectId)
    {
        _positions.TryGetValue(objectId, out var state);
        return state;
    }
}
