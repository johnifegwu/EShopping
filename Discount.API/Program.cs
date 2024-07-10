using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Discount.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Discount.Core.Entities;
using Discount.Application.Commands;
using Discount.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//=====================================================================================================
builder.Services.AddControllers();
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

var conString = builder.Configuration["ConnectionStrings:DiscountDbConnection"];
builder.Services.AddHealthChecks().AddNpgSql(conString, name:"Catalog PostgreSql Helth Check", tags: new[] { "Discount"});

//Add gRPC
builder.Services.AddGrpc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//=====================================================================================================

// Add depencies
//=====================================================================================================
var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Coupon)), // Core
  Assembly.GetAssembly(typeof(CreateCouponCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
//=====================================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client");

app.Run();
