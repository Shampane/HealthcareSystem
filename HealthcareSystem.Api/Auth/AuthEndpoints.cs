using HealthcareSystem.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Auth;

public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/register", async (
            [FromBody] UserRegisterRequest request,
            AuthService service
        ) => await service.UserRegister(request)).WithTags("Auth");

        return app;
    }
}