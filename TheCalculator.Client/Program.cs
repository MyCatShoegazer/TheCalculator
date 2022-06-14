using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheCalculator.Client;
using TheCalculator.Client.Services;
using TheCalculator.Client.Services.Abstract;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IOptionsProviderService, OptionsProviderService>();

builder.Services.AddScoped(sp =>
{
    var optionsProvider = sp.GetRequiredService<IOptionsProviderService>();
    var options = optionsProvider.GetApiOptions();

    if (options.ApiBasePath != null)
    {
        return new HttpClient {BaseAddress = new Uri(options.ApiBasePath)};
    }

    throw new ApplicationException("API base path was not found!");
});

builder.Services.AddScoped<IHttpComputingService, HttpComputingService>();

await builder.Build().RunAsync();
