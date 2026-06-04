using FoodOrdering.API.Middleware;
using FoodOrdering.API.Utilities;
using FoodOrdering.Service.Abstractions;
using FoodOrdering.Service.Implementations;
using FoodOrdering.Store.Abstractions;
using FoodOrdering.Store.Implementations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// JWT Configuration
var jwtKey = builder.Configuration["JwtSettings:Key"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["JwtSettings:Issuer"],

            ValidAudience =
                builder.Configuration["JwtSettings:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey))
        };
});

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "FoodOrdering API",
            Version = "v1",
            Description =
                "Food Ordering Application APIs with JWT Authentication"
        });

    // XML Documentation Support
    var xmlFile =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            xmlFile);

    c.IncludeXmlComments(xmlPath);

    // JWT Swagger Authentication
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "Enter JWT Token. Example: Bearer {your token}",

            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });

    // Controller Ordering
    c.OrderActionsBy(apiDesc =>
    {
        var controller =
            apiDesc.ActionDescriptor
                .RouteValues["controller"];

        return controller switch
        {
            "Auth" => "1",
            "Admin" => "2",
            "User" => "3",
            _ => "9"
        };
    });
});

// Dependency Injection
builder.Services.AddScoped<
    IFoodItemStore,
    FoodItemStore>();

builder.Services.AddScoped<
    IFoodItemService,
    FoodItemService>();

builder.Services.AddScoped<
    IUserStore,
    UserStore>();

builder.Services.AddScoped<
    IUserService,
    UserService>();

builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

// Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Food Ordering API";
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "FoodOrdering API V1");
    });
}

app.UseHttpsRedirection();

// Global Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();