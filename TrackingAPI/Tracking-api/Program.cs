/* Entry point of the app - here it is bootstraped and registers the modules for dependency injection using builder.Services. */

using Microsoft.EntityFrameworkCore;
using Tracking_api.Data;
using Tracking_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IssueDbContext>(  // DbContext is added to the service container with specified options and can be istantiated per request
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));  // option UseSqlServer sets a connection str to DB (from appsettings.json)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
