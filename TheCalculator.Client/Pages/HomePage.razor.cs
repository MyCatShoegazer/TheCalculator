using Microsoft.AspNetCore.Components;
using TheCalculator.Client.Services.Abstract;
using TheCalculator.Client.Shared.Components;
using TheCalculator.Domain.InputModels;
using TheCalculator.Domain.ViewModels;

namespace TheCalculator.Client.Pages;

/// <summary>
///     A model for home page.
/// </summary>
public partial class HomePage : BaseCalculatorComponent
{
    private bool _computing = false;
    private List<SquareChainVm> _viewModels = new();
    private string? _inputText = string.Empty;

    [Inject]
    public IHttpComputingService HttpComputingService { get; set; } = null!;

    protected override void OnInitialized()
    {
        base.SwitchReady();
        base.OnInitialized();
    }

    private void RemoveItemFromList(SquareChainVm vm)
    {
        this._viewModels.Remove(vm);
        this.StateHasChanged();
    }

    private void ClearItemList()
    {
        this._viewModels.Clear();
        this._inputText = string.Empty;
        this.StateHasChanged();
    }

    private async Task ComputeAsync()
    {
        this._computing = true;
        this.StateHasChanged();

        try
        {
            var values = this._inputText?.Normalize().Split(',').Select(x =>
                    double.TryParse(x, out var value) ? value : 0)
                .Where(x => x != 0)
                .ToArray();

            if (values is null) return;

            var inputModel = new SquareChainInputModel {Values = values};
            var result =
                await this.HttpComputingService.GetComputedSquaresAsync(
                    inputModel);

            if (result is not null)
                this._viewModels.Add(result);
        }
        catch
        {
            this._inputText = string.Empty;
        }
        finally
        {
            _computing = false;
            this.StateHasChanged();
        }
    }
}
