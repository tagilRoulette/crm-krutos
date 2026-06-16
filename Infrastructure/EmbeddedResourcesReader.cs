using System.Data.SqlTypes;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Crm.Infrastructure;

public static class EmbeddedResourcesReader
{
    public static T? ReadJson<T>(string name, EmbeddedFileProvider fileProvider)
    {
        var fileInfo = fileProvider.GetFileInfo(name);
        if (!fileInfo.Exists) throw new FileNotFoundException("Resource not found.");

        using Stream stream = fileInfo.CreateReadStream();
        using StreamReader streamReader = new(stream);
        using JsonTextReader reader = new(streamReader);
        var serializer = JsonSerializer.Create();
        return serializer.Deserialize<T>(reader);
    }
}
