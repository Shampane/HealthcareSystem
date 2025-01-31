using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Endpoints;

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
                [FromBody] Token request,
                AuthService service
            ) => await service.RefreshToken(request)
        ).WithTags("Auth");

        return app;
    }
}