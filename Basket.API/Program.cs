using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.Configurations;
using Basket.Core.Entities;
using Basket.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using static Discount.Grpc.Protos.DiscountProtoService;

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

//Add default config
builder.Services.Configure<DefaultConfig>(builder.Configuration.GetSection("configs"));

var conString = builder.Configuration["configs:redisurl"];
builder.Services.AddHealthChecks().AddRedis(conString, "Basket Redis Helth Check", HealthStatus.Degraded);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
    x.SwaggerDoc("v1", new OpenApiInfo() { Title = "eShopping Basket ® API", Version = "v1" });
});
//=====================================================================================================

// Add depencies
//=====================================================================================================
var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(ShoppingCart)), // Core
  Assembly.GetAssembly(typeof(CreateOrUpdateShoppingCartCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
var RpcAddress = builder.Configuration["configs:discountRpcHost"];
builder.Services
    .AddGrpcClient<DiscountProtoServiceClient>(o =>
    {
        o.Address = new Uri($"https://{RpcAddress}");
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });
//=====================================================================================================
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.RoutePrefix = "swagger";
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "eShopping Basket ® API");
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
