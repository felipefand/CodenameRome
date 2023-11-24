using CodenameRome.Models;
using CodenameRome.Services;

var builder = WebApplication.CreateBuilder(args);

// Recover Database Configuration Settings
builder.Services.Configure<DBSettings>(
    builder.Configuration.GetSection("CodenameRomeDB"));

// Add DB Services
builder.Services.AddSingleton<MenuService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
