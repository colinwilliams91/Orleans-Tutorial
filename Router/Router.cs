using OrleansURLShortener.Extensions;

namespace OrleansURLShortener.Router;

/// <summary>
/// Maps all endpoints for CRUD by calling extension methods
/// </summary>
public class Router(WebApplication app)
{
    public WebApplication App { get; set; } = app;

    public void MapEndpoints()
    {
        this.App.MapGetEndpoints();
        // TODO: MapPostEndpoints, MapPutEndpoints, MapDeleteEndpoints...
    }
}
