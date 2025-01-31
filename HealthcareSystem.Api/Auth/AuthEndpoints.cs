using HealthcareSystem.Application.Auth;
using HealthcareSystem.Core.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Auth;

public static class AuthEndpoints {
    public static WebApplication
        MapAuthEndpoints(this WebApplication app) {
        app.MapPost(
            "/api/register", async (
                [FromBody] UserRegisterRequest request,
                AuthService service
            ) => await service.Register(request)
        ).WithTags("Auth");

        app.MapPost(
            "/api/login", async (
                [FromBody] UserAuthenticateRequest request,
                AuthService service
            ) => await service.Authenticate(request)
        ).WithTags("Auth");

        app.MapPost(
            "/api/refreshToken", async (
                [FromBody] TokenDto request,
                AuthService service
            ) => await service.RefreshToken(request)
        ).WithTags("Auth");

        return app;
    }
}