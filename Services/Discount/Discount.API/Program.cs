using System.Reflection;
using Discount.Infrastructure.Extensions;
using Discount.Core.Entities;
using Discount.Application.Commands;
using Discount.Application.Services;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Discount.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

//Add gRPC
//=====================================================================================================
builder.Services.AddGrpc();
//=====================================================================================================

// Add depencies
//=====================================================================================================
builder.Services.AddInfrastructure(builder.Configuration);
var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Coupon)), // Core
  Assembly.GetAssembly(typeof(CreateCouponCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddAutoMapper(typeof(Program));
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps("aspnetapp.pfx",
            "12345678");
    });
});
//=====================================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    var scope = app.Services.CreateScope();
    var seed = scope.ServiceProvider.GetRequiredService<IDiscountSeeder>();
    await seed.Seed();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client");

app.Run();
