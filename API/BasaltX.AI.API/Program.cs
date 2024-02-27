using BasaltX.AI.BL;
using BasaltX.AI.Api.Configurations;
using BasaltX.Models.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.AddAllowedOriginsConfiguration(builder.Configuration);


//Let validate that the settings are set on the appSettings to avoid application crash
builder.Services.AddOptions<RapidApiSettings>()
    .Bind(builder.Configuration
    .GetSection(RapidApiSettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();


//Register the AI Module
builder.Services.AddAIModuleCollection();

var app = builder.Build();

app.MapHealthChecks("health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");
//Register the AI endpoints Module
app.AddEndPointsConfiguration();

app.Run();
