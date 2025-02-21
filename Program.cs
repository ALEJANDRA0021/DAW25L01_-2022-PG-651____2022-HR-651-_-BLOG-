using L01_NUMEROS_CARNETS.Models;
using Microsoft.EntityFrameworkCore;


var buildr = WebApplication.CreateBuilder(args);

// Add services to the container.

buildr.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
buildr.Services.AddDbContext<BlogDBContext>(opciones =>
    opciones.UseSqlServer(buildr.Configuration.GetConnectionString("Conectar")));

buildr.Services.AddEndpointsApiExplorer();
buildr.Services.AddSwaggerGen();

var app = buildr.Build();

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