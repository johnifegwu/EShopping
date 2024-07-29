using Ordering.Infrastructure.Extensions;
using System.Reflection;
using eShopping.ExceptionHandling;
using Ordering.Core.Entities;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Ordering.Application.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using eShopping.MailMan.Extensions;
using MassTransit;
using eShopping.MessageBrocker.Extensions;
using eShopping.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

//Add config
builder.Services.Configure<DefaultConfig>(builder.Configuration.GetSection("configs"));

//Add Exception handlers
builder.Services.AddExceptionHadlers();

//Add RabbitMQ
builder.Services.AddRabbitMQService(builder.Configuration.GetSection("MessageBrocker"));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri($"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? builder.Configuration["MessageBrocker:Host"]}:{Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? builder.Configuration["MessageBrocker:Port"]}"), h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? builder.Configuration["MessageBrocker:UserName"]);
            h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? builder.Configuration["MessageBrocker:Password"]);
        });
    });
});

//Add emailService
builder.Services.AddEmailService(builder.Configuration, "EmailSettings", Assembly.GetExecutingAssembly());

var conString = builder.Configuration["ConnectionStrings:OrderingDbConnection"];
builder.Services.AddHealthChecks().AddSqlServer(conString, "Catalog Sql Server Helth Check", tags: new[] { "infra" });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "eShopping Ordering ® API", Version = "v1" });
    // Add the JWT Bearer token configuration to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Order)), // Core
  Assembly.GetAssembly(typeof(CreateOrderCommand)) // Application
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrCustomer", policy => policy.RequireRole("Admin", "Customer"));
});

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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add the authentication middleware

app.UseAuthorization(); // Add the authorization middleware

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();