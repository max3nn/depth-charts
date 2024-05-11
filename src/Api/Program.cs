using DepthChart.Application;
using DepthChart.Infrastructure.Data;
using WebApplicationBuilder = Microsoft.AspNetCore.Builder;

var builder = WebApplicationBuilder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

// TODO: 
// Implement global error handling
// Implement global logging
// Implement global exception handling
// Implement global validation
// Implement global security
// Implement global caching
// Implement global rate limiting
// Implement global monitoring
// Implement global health checks
// Implement global metrics

// app.UseAuthorization();


app.MapControllers();

// app.UseCors();

app.Run();
