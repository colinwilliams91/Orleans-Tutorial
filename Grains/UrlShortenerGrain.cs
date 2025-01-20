using OrleansURLShortener.Interfaces;
using OrleansURLShortener.Models;

namespace OrleansURLShortener.Grains;

public sealed class UrlShortenerGrain(
    [PersistentState(stateName: "url", storageName: "urls")]
    IPersistentState<UrlDetails> state) : Grain, IUrlShortenerGrain
{
    public async Task SetUrl(string fullUrl)
    {
        state.State = new()
        {
            ShortenedRouteSegment = this.GetPrimaryKeyString(),
            FullUrl = fullUrl
        };

        await state.WriteStateAsync();
    }

    public Task<string> GetUrl() => Task.FromResult(state.State.FullUrl);
}
