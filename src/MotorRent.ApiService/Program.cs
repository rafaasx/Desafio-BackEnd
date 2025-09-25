using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MotorRent.ApiService.Filters;
using MotorRent.ApiService.Middlewares;
using MotorRent.Application;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.DTO.Moto;
using MotorRent.Infrastructure;
using MotorRent.OutOfBox.Database;
using MotorRent.OutOfBox.Queues;
using MotorRent.ServiceDefaults;
using System.Reflection;

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
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoRent API",
        Version = "v1",
        Description = "API para gestão de locação de motos",
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddPostgresDbContext<ApplicationDbContext>(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddInfraServices();
builder.Services.AddValidators();
builder.Services.AddConsumers();

builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder()
                    .Add<CreateMotoDTO>()
                    .Add<CreateEntregadorDTO>()
                    .Add<Created2024MotoDTO>());



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseMiddleware<ExceptionHandlerMiddleware>();

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
