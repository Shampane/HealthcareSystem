using HealthcareSystem.Api.Email;
using HealthcareSystem.Api.Endpoints;
using HealthcareSystem.Api.Extensions;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Infrastructure.DataAccess;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddEmailServices(builder.Configuration);
builder.Services.AddDoctorServices();
builder.Services.AddScheduleServices();
builder.Services.AddAppointmentServices();
builder.Services.AddHostedService<ScheduleCleanupService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapDoctorEndpoints();
app.MapScheduleEndpoints();
app.MapAuthEndpoints();
app.MapAppointmentEndpoints();

await app.RunAsync();