using Microsoft.OpenApi.Models;

namespace GameVault.Authentication;

public static class SwaggerConfiguration
{
    public static OpenApiSecurityScheme OpenApiSecurityScheme => new()
    {
        Description = "The Api Key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthConstants.ApiKeyHeaderName,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    };

    private static OpenApiSecurityScheme Scheme => new()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    public static OpenApiSecurityRequirement Requirement => new()
    {
        {Scheme, new List<string>() }
    };
}
