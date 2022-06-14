using System.Net.Http.Json;
using TheCalculator.Client.Services.Abstract;
using TheCalculator.Domain.Infrastructure;
using TheCalculator.Domain.InputModels;
using TheCalculator.Domain.ViewModels;

namespace TheCalculator.Client.Services;

public class HttpComputingService : IHttpComputingService
{
    private readonly HttpClient _httpClient;
    private readonly IOptionsProviderService _optionsProvider;

    public HttpComputingService(HttpClient httpClient,
        IOptionsProviderService optionsProvider)
    {
        _httpClient = httpClient;
        _optionsProvider = optionsProvider;
    }

    public async Task<SquareChainVm?> GetComputedSquaresAsync(
        SquareChainInputModel squareChainInputModel,
        CancellationToken cancellationToken = default)
    {
        var options = this._optionsProvider.GetApiOptions();

        if (options.Routes is null)
            throw new ApplicationException("Route not found!");

        var response = await this._httpClient.PostAsJsonAsync(
            options.Routes.SquareChainCompute, squareChainInputModel,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException("Can't request data!");
        }

        var vms =
            await response.Content
                .ReadFromJsonAsync<ApiResponseModel<SquareChainVm>>(
                    cancellationToken: cancellationToken);

        return vms?.Payload;
    }
}
