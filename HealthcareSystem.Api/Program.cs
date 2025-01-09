using HealthcareSystem.Api.Auth;
using HealthcareSystem.Api.Doctors;
using HealthcareSystem.Api.Schedules;
using HealthcareSystem.Application.Schedules;
using HealthcareSystem.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.ConfigureAuth();

builder.Services.AddDoctorServices();
builder.Services.AddSchedulesServices();
builder.Services.AddHostedService<ScheduleCleanupService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapDoctorEndpoints();
app.MapScheduleEndpoints();

await app.RunAsync();