using HealthcareSystem.Api.Extensions;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Infrastructure.DataAccess;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddMyExceptionHandlers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureEmail(builder.Configuration);

builder.Services.AddMyValidators();
builder.Services.AddMyRepositories();
builder.Services.AddHostedService<ScheduleCleanupService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers().AddNewtonsoftJson();

WebApplication app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    //.WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseExceptionHandler();

app.MapControllers();
app.AddScalarApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();