using Microsoft.Extensions.FileProviders;

namespace Crm.Infrastructure;

public static class EmbeddedResourcesReader
{
    public static string ReadJson(string name, EmbeddedFileProvider fileProvider)
    {
        //Assembly asm = Assembly.GetExecutingAssembly();
        //string resourceName = asm
        //    .GetManifestResourceNames()
        //    .Single(str => str.EndsWith(name));

        var fileInfo = fileProvider.GetFileInfo(name);
        if (fileInfo.Exists) throw new FileNotFoundException("Resource not found.");

        using Stream stream = fileInfo.CreateReadStream();
        //?? throw new FileNotFoundException("Resource not found.");
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}
