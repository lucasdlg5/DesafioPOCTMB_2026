using System.Web.Http;
using TMB_REST;
using TMB_REST.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OrderContext") 
    ?? throw new InvalidOperationException("Connection string 'OrderContext' not found.");

// Registra o DbContext uma única vez
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                                .AllowAnyMethod()    // Permite POST, PUT, DELETE, OPTIONS etc.
                                .AllowAnyHeader();   // Permite headers de Content-Type, Authorization, etc.
                                // .AllowCredentials(); // habilite se precisar enviar cookies/credenciais
                      });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
// Aplicar a política de CORS antes dos controllers
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
app.Run();