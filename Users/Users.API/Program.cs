using Asp.Versioning;
using eShopping.ExceptionHandling;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;
using Users.Application.Commands;
using Users.Core.Entities;
using Users.Core.Models;
using Users.Infrastructure.Extensions;
using Users.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

//Add Exception handlers
builder.Services.AddExceptionHadlers();

//Add default config
builder.Services.Configure<DefaultConfig>(builder.Configuration.GetSection("configs"));
//Add Exception handlers
builder.Services.AddExceptionHadlers();

var conString = builder.Configuration["ConnectionStrings:UsersDbConnection"];
builder.Services.AddHealthChecks().AddMySql(conString, "Users MySql Helth Check", tags: new[] { "infra" });

// Add depencies
//=====================================================================================================
var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(User)), // Core
  Assembly.GetAssembly(typeof(CreateAdminUserCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.RoutePrefix = "swagger";
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "eShopping Users ® API");
    });
    app.UseDeveloperExceptionPage();
    app.ApplyMigrations();
    var scope = app.Services.CreateScope();
    var seed = scope.ServiceProvider.GetRequiredService<IUsersSeeder>();
    await seed.Seed();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
