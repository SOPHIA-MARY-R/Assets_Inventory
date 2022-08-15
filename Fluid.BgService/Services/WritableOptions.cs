using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;

namespace Fluid.BgService.Services;

public class WritableOptions<T> where T : class, new()
{
    private readonly IHostEnvironment _environment;
    private readonly IOptionsMonitor<T> _options;
    private readonly IConfigurationRoot _configuration;
    private readonly string _section;
    private readonly string _file;

    public T Value => _options.CurrentValue;

    public T Get(string name) => _options.Get(name);

    public WritableOptions(IHostEnvironment environment, IOptionsMonitor<T> options, IConfigurationRoot configuration, string section, string file)
    {
        _environment = environment;
        _options = options;
        _configuration = configuration;
        _section = section;
        _file = file;
    }

    public void Update(Action<T> applyChanges)
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo(_file);
        var physicalPath = fileInfo.PhysicalPath;

        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        var jsonObject = JsonSerializer.Deserialize<JsonObject>(File.ReadAllBytes(physicalPath), jsonSerializerOptions);
        var section = jsonObject!.TryGetPropertyValue(_section, out JsonNode node) ? JsonSerializer.Deserialize<T>(node!.ToString()) : Value ?? new T();

        applyChanges(section);

        jsonObject[_section] = JsonNode.Parse(JsonSerializer.Serialize(section));
        File.WriteAllText(physicalPath, JsonSerializer.Serialize(jsonObject, jsonSerializerOptions));
        _configuration.Reload();
    }
}