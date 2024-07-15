using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ordering.Infrastructure.Extensions;
using System.Reflection;
using eShopping.ExceptionHandling;
using Ordering.Core.Entities;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Ordering.Application.Commands;

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

var conString = builder.Configuration["ConnectionStrings:OrderingDbConnection"];
builder.Services.AddHealthChecks().AddSqlServer(conString, "Catalog Sql Server Helth Check", tags: new[] { "infra" });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Order)), // Core
  Assembly.GetAssembly(typeof(CreateOrderCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.RoutePrefix = "swagger";
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "eShopping Ordering ® API");
    });
    app.UseDeveloperExceptionPage();
    app.ApplyMigrations();
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
