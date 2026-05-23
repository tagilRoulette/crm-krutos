using System.Collections.Concurrent;

namespace Crm.Logic.Layout;

public class LayoutStateManager
{
    private readonly ConcurrentDictionary<Guid, string> _positions = new();

    public void UpdateState(Guid objectId, string json)
    {
        _positions.AddOrUpdate(
            objectId,
            json,
            (_, _) => json);
    }

    public string? GetElementState(Guid objectId)
    {
        _positions.TryGetValue(objectId, out var state);
        return state;
    }
}
