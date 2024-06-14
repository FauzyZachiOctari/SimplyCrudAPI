using Microsoft.EntityFrameworkCore;
using SimplyCrudAPI.Models.User;
using SimplyCrudAPI.Models;
using SimplyCrudAPI.Models.Book;

namespace SimplyCrudAPI.Data
{
    public class BookAPIDbContext : DbContext
    {
        public BookAPIDbContext(DbContextOptions<BookAPIDbContext> options) : base(options)
        {
        }

        public DbSet<BookDataList> BookDataListed { get; set; }
    }
}