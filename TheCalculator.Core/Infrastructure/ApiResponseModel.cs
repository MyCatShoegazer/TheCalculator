namespace TheCalculator.Core.Infrastructure;

public class ApiResponseModel<T> : ApiModelBase
{
    public ApiResponseModel(T? payload)
    {
        this.Payload = payload;
    }

    public T? Payload { get; }
}
