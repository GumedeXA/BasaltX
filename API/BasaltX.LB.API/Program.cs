using BasaltX.LB.BL;
using BasaltX.LB.API.Configurations;
using BasaltX.Models.Models.Settings;
using BasaltX.LB.BL.Features.Caching.Implementation.Redis.Extenstions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

//Register cors logic
builder.AddAllowedOriginsConfiguration(builder.Configuration);

//Register the Local Business Module to the DI Container
builder.Services.AddLBModuleCollection(builder.Configuration);

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

app.UseCors("CorsPolicy");
//Register the Local Business endpoints Module
app.AddEndPointsConfiguration();

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseAuthorization();

app.Run();
