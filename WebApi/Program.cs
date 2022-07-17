using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<WebApiContext>(opt =>
    opt.UseInMemoryDatabase("Contato"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
