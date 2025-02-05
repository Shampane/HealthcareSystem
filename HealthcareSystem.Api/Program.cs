using HealthcareSystem.Api.Extensions;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Infrastructure.DataAccess;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddAuthServices();

builder.Services.ConfigureEmail(builder.Configuration);
builder.Services.AddEmailServices();
builder.Services.AddDoctorServices();
builder.Services.AddScheduleServices();
builder.Services.AddAppointmentServices();
builder.Services.AddHostedService<ScheduleCleanupService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.MapScalarApiReference(
        options => {
            options.Servers = [
                new ScalarServer("https://localhost:8081")
            ];
        }
    );
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();