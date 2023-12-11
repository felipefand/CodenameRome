using CodenameRome.Application;
using CodenameRome.Application.Auth;
using CodenameRome.Application.Interfaces;
using CodenameRome.Auth;
using CodenameRome.Database;
using CodenameRome.Services;
using CodenameRome.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Recover Database Configuration Settings
builder.Services.Configure<DBSettings>(
    builder.Configuration.GetSection("CodenameRomeDB"));

builder.Services.Configure<TokenSettings>(
    builder.Configuration.GetSection("TokenSettings"));

// Add Application Services
builder.Services.AddSingleton<ILoginApplication, LoginApplication>();
builder.Services.AddSingleton<IClientApplication, ClientApplication>();
builder.Services.AddSingleton<IEmployeeApplication, EmployeeApplication>();
builder.Services.AddSingleton<IProductApplication, ProductApplication>();

// Add DB Services
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IProductService, ProductService>();

//Dependency Injection
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddSingleton<DatabaseFilters>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add SwaggerGen to ask for JWT token.
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informe o token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// JWT Auth

var tokenSettings = new TokenSettings();
new ConfigureFromConfigurationOptions<TokenSettings>(builder.Configuration.GetSection("TokenSettings")).Configure(tokenSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = tokenSettings.Audience,
        ValidIssuer = tokenSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret!))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();