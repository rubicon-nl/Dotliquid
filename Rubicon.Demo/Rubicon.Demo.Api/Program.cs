using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Rubicon.Demo.Api.Configuration;
using Rubicon.Demo.Api.StartupExtensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add keyvault
var keyvaultConfig = builder.Configuration.GetSection("KeyVault").Get<KeyvaultOptions>();

builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{keyvaultConfig.Vault}.vault.azure.net/"),
    new ClientSecretCredential(keyvaultConfig.TenantId, keyvaultConfig.ClientId, keyvaultConfig.ClientSecret));

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddCors();
builder.Services.AddControllers()
    .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration, builder.Environment);
builder.Services.AddSqlDb(builder.Configuration);

var authOptions = builder.Configuration.GetSection("Settings").GetSection("AzureAd").Get<AzureAdOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Authority = $"{authOptions.Instance}/{authOptions.TenantId}/";
        options.Audience = authOptions.Audience;
    });

builder.Services.AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseSqlDb();
app.UseSwagger(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();