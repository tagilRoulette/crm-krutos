using System.Collections.Concurrent;

namespace Crm.Logic.Layout;

public class LayoutStateManager
{
    private readonly ConcurrentDictionary<Guid, string> _positions = new();
    private readonly ILogger<LayoutStateManager> _logger;
    public LayoutStateManager(ILogger<LayoutStateManager> logger)
    {
        _logger = logger;
    }
    public void AddOrUpdateState(Guid objectId, string json)
    {
        
        _positions.AddOrUpdate(
            objectId,
            json,
            (_, _) => json);
        foreach (var kvp in _positions)
        {
            _logger.LogInformation($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
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
        _positions.TryGetValue(objectId, out string state);
        return state;
    }
}
