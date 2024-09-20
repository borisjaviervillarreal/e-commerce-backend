using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddCustomServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar redirección HTTPS si está habilitado.
app.UseHttpsRedirection();

// Usar enrutamiento
app.UseRouting();

// Aplicar autenticación y autorización antes de Ocelot
app.UseAuthentication();
app.UseAuthorization();

app.Run();

