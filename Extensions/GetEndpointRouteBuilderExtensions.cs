﻿using OrleansURLShortener.Interfaces;

namespace OrleansURLShortener.Extensions;

public static class GetEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Home Endpoint - landing screen endpoint
    /// </summary>
    public static void MapGetHome(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", static () => "URL Shortener powered by Orleans.")
            .WithName("Home")
            .WithOpenApi();
    }

    /// <summary>
    /// Shorten Endpoint - creates or gets, and stores version of provided URL in grain state
    /// </summary>
    public static void MapGetShorten(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/shorten",
            static async (IGrainFactory grains, HttpRequest request, string url) =>
            {
                var host = $"{request.Scheme}://{request.Host.Value}";

                // Validate the URL query string.
                if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    return Results.BadRequest($"""
                        The URL query string is required and needs to be well formed.
                        Consider, ${host}/shorten?url=https://www.microsoft.com.
                        """);
                }

                // Create a unique, short ID
                var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

                // Create and persist a grain with the shortened ID and full URL
                var shortenerGrain =
                    grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

                await shortenerGrain.SetUrl(url);

                // Return the shortened URL for later use
                var resultBuilder = new UriBuilder(host)
                {
                    Path = $"/go/{shortenedRouteSegment}"
                };

                return Results.Ok(resultBuilder.Uri);
            })
            .WithName("Shorten")
            .WithOpenApi();
    }

    /// <summary>
    /// Go Endpoint - gets full url by short code grain state and redirects
    /// </summary>
    public static void MapGetGo(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/go/{shortenedRouteSegment:required}",
            static async (IGrainFactory grains, string shortenedRouteSegment) =>
            {
                // Retrieve the grain using the shortened ID and url to the original URL
                var shortenerGrain =
                    grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

                var url = await shortenerGrain.GetUrl();

                // Handles missing schemes, defaults to "http://".
                var redirectBuilder = new UriBuilder(url);

                return Results.Redirect(redirectBuilder.Uri.ToString());
            })
            .WithName("Go")
            .WithOpenApi();
    }
}
