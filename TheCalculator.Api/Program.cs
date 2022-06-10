using System.Text.Json;
using System.Text.Json.Serialization;
using TheCalculator.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOptions();
builder.ConfigureAutomapper();
builder.AddApiServices();
builder.AddApiFilters();

builder.Services.AddControllers().AddJsonOptions(configure =>
{
    var jsonOptions = configure.JsonSerializerOptions;
    jsonOptions.WriteIndented = false;
    jsonOptions.AllowTrailingCommas = false;
    jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    jsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    jsonOptions.PropertyNameCaseInsensitive = false;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
