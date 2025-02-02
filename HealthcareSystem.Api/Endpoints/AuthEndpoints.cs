using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Endpoints;

public static class AuthEndpoints {
    public static WebApplication MapAuthEndpoints(this WebApplication app) {
        app.MapPost("/api/register", async (
            [FromBody] UserRegisterRequest request,
            AuthService service
        ) => await service.Register(request)).WithTags("Auth");

        app.MapPost("/api/login", async (
            [FromBody] UserAuthenticateRequest request,
            AuthService service
        ) => await service.Authenticate(request)).WithTags("Auth");

        app.MapPost("/api/refreshToken", async (
                [FromBody] Token request,
                AuthService service
            ) => await service.RefreshToken(request))
            .RequireAuthorization("UserPolicy")
            .WithTags("Auth");

        app.MapPost("/api/forgetPassword", async (
                [FromBody] ForgetPasswordRequest request,
                AuthService service
            ) => await service.ForgetPassword(request))
            .WithTags("Auth");

        app.MapPost("/api/resetPassword", async (
                [FromBody] ResetPassword request,
                AuthService service
            ) => await service.ResetPassword(request))
            .WithTags("Auth");

        return app;
    }
}