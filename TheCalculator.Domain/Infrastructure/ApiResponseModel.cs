namespace TheCalculator.Domain.Infrastructure;

/// <summary>
///     An API response model which represents normal object response.
/// </summary>
/// <typeparam name="T">Response type.</typeparam>
public class ApiResponseModel<T> : ApiModelBase
{
    /// <summary>
    ///     Creates a new instance of API response model.
    /// </summary>
    /// <param name="payload">Response payload.</param>
    public ApiResponseModel(T? payload)
    {
        this.Payload = payload;
    }

    /// <summary>
    ///     Response payload.
    /// </summary>
    public T? Payload { get; }
}
