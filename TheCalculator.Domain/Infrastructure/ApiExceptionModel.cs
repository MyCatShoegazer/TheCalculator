namespace TheCalculator.Domain.Infrastructure;

/// <summary>
///     An API response model which represents unhandled exception.
/// </summary>
public class ApiExceptionModel : ApiModelBase
{
    /// <summary>
    ///     Creates a new instance of exception response model.
    /// </summary>
    /// <param name="exception">An application occured exception.</param>
    /// <param name="isDevelopment">
    ///     Set to <see langword="true" />
    ///     if application is in development mode.
    /// </param>
    public ApiExceptionModel(Exception exception, bool isDevelopment = false)
    {
        this.Message = exception.Message;

        if (isDevelopment)
            this.StackTrace = exception.StackTrace;
    }

    /// <summary>
    ///     Exception message.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    ///     Exception stack.
    /// </summary>
    public string? StackTrace { get; } = "Not supported.";
}
