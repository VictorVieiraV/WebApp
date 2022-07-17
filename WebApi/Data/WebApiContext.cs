using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApi.Data
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }
        public DbSet<Contato> Contato { get; set; } = null!;
    }
}
