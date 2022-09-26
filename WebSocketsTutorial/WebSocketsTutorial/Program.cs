var builder = WebApplication.CreateBuilder(args);

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
    app.UseWebSockets();  // In Tutorial Startup.cs file is referenced for this. Sends Upgrade to WebSocket conntection requests when sending HTTPs GET request 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
