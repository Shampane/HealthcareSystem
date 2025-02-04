using HealthcareSystem.Api.Extensions;
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
//builder.Services.AddHostedService<ScheduleCleanupService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

WebApplication app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.MapScalarApiReference(
        options => options.Servers = [
            new ScalarServer("https://localhost:8081")
        ]
    );
}
/*
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

/*
app.MapDoctorEndpoints();
app.MapScheduleEndpoints();
app.MapAuthEndpoints();
app.MapAppointmentEndpoints();
*/

await app.RunAsync();