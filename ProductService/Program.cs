using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddCustomServices();

// Agregar Swagger para la documentaci�n de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar redirecci�n HTTPS si est� habilitado.
app.UseHttpsRedirection();

// Usar enrutamiento
app.UseRouting();

// Aplicar autenticaci�n y autorizaci�n antes de Ocelot
app.UseAuthentication();
app.UseAuthorization();

app.Run();

