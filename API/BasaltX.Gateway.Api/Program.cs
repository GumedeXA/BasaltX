using BasaltX.Utils;
using BasalX.Service.Agents;
using BasaltX.Gateway.Api.Configurations;
using BasalX.Service.Agents.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddUtilitiesModuleCollection();
builder.Services.AddBasaltXServiceAgentsModuleCollection();

//Let validate that the settings are set on the appSettings to avoid application crash
builder.Services.AddOptions<InternalApiSettings>()
    .Bind(builder.Configuration
    .GetSection(InternalApiSettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.AddEndPointsConfiguration();


app.Run();
