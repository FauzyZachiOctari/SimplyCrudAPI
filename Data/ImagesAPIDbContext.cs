using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimplyCrudAPI.Models.Images;

namespace SimplyCrudAPI.Data
{
    public class ImagesAPIDbContext : DbContext
    {
        public ImagesAPIDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Images> Imagined { get; set; }
    }
}
