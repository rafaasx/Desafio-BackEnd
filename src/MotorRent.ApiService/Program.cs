using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MotorRent.ApiService.Filters;
using MotorRent.Infrastructure;
using MotorRent.OutOfBox.Database;
using MotorRent.OutOfBox.Queues;
using MotorRent.OutOfBox.Repositories;
using MotorRent.OutOfBox.Services;
using MotorRent.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoRent API",
        Version = "v1",
        Description = "API para gestão de locação de motos",
    });
});
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddPostgresDbContext<ApplicationDbContext>(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddBus(builder.Configuration);



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoRent API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
