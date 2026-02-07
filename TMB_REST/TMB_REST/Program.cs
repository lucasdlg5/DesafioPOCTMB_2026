using TMB_REST;
using TMB_REST.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<OrderContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/test", () => "Hello World!");

app.OrdersRoutes();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
