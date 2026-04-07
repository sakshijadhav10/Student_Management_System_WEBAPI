using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Student_Management_System_API.Middleware;
using Student_Management_System_BLL.Interfaces;
using Student_Management_System_BLL.Services;
using Student_Management_System_DAL.Data;
using Student_Management_System_DAL.Interfaces;
using Student_Management_System_DAL.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------
// Serilog
// ---------------------------------------------
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// ---------------------------------------------
// Database
// ---------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Student_Management_System_API")
    ));


// ---------------------------------------------
// Repository Layer
// ---------------------------------------------
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


// ---------------------------------------------
// Service Layer
// ---------------------------------------------
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();

// ---------------------------------------------
// JWT Authentication
// ---------------------------------------------
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


// ---------------------------------------------
// Swagger + JWT Authorization
// ---------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Student Management API",
        Version = "v1"
    });

    // Add JWT auth to swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter token like: Bearer eyJhbGci..."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();


// ---------------------------------------------
// Build App
// ---------------------------------------------
var app = builder.Build();


// ---------------------------------------------
// Middleware Pipeline
// ---------------------------------------------
app.UseSwagger();
app.UseSwaggerUI();

// Global exception handler
app.UseMiddleware<ExceptionMiddleware>();

// Serilog request logging
app.UseSerilogRequestLogging();

// Authentication + Authorization
app.UseAuthentication();   // MUST COME FIRST
app.UseAuthorization();

app.MapControllers();

app.Run();