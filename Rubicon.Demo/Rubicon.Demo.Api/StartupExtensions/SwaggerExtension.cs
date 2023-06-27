using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rubicon.Demo.Api.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var authOptions = configuration.GetSection("Settings").GetSection("AzureAd").Get<Configuration.AzureAdOptions>();

            services.AddSwaggerGen(
                options =>
                {
                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                    // exclude internal urls on not development environments

                    if (!environment.IsDevelopment())
                    {
                        options.DocumentFilter<ExcludeInternalRoutesDocumentsFilter>();
                    }

                    options.AddSecurityDefinition("oauth2",
                        new OpenApiSecurityScheme
                        {
                            Description = "Select scope",
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{authOptions.TenantId}/oauth2/v2.0/authorize"),
                                    TokenUrl = new Uri($"https://login.microsoftonline.com/{authOptions.TenantId}/oauth2/v2.0/token"),
                                    Scopes = new Dictionary<string, string> {
                                        { $"{authOptions.ClientId}/.default" , "Rubicon Demo - API" }
                                    }
                                }
                            }
                        });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme {
                                    Name = "oauth2",
                                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                                }, new List<string> { }
                            }
                        });
                });
        }

        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("Settings").GetSection("AzureAd").Get<Configuration.AzureAdOptions>();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((openApiDoc, httpReq) =>
                {
                    openApiDoc.Servers.Add(new OpenApiServer
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}",
                        Description = "Rubicon Demo - API"
                    });
                });
            });

            app.UseSwaggerUI(
                options =>
                {
                    options.OAuthClientId(authOptions.ClientId);
                });
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                return Path.Combine(AppContext.BaseDirectory, fileName);
            }
        }
    }

    public class ExcludeInternalRoutesDocumentsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var internalRoutes = swaggerDoc.Paths
                .Where(x => x.Key.ToLower().Contains("api/internal/"))
                .ToList();

            internalRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}
