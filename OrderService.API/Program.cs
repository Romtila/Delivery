using Microsoft.OpenApi.Models;
using OrderService.API;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.RegisterServices(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Delivery.OrderService.API", Version = "v1"}); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery.OrderService.API v1"));
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureApp();

app.Run();