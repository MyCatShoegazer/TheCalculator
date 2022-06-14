using Microsoft.AspNetCore.Components;

namespace TheCalculator.Client.Shared.Components;

/// <summary>
///     A base component for other UI components.
/// </summary>
public partial class BaseCalculatorComponent : ComponentBase
{
    protected bool _isReady = false;

    protected void SwitchReady(bool isReady = true)
    {
        this._isReady = isReady;
        this.StateHasChanged();
    }
}
