using BasaltX.LB.BL;
using BasaltX.LB.API.Configurations;
using BasaltX.Models.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

//Register the Movies Module to the DI Container
builder.Services.AddLBModuleCollection();

//Let validate that the settings are set on the appSettings to avoid application crash
builder.Services.AddOptions<RapidApiSettings>()
    .Bind(builder.Configuration
    .GetSection(RapidApiSettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//Query if the api is up and running
app.MapHealthChecks("health");

//Register the Movies endpoints Module
app.AddEndPointsConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
