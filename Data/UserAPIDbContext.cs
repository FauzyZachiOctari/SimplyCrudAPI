using SimplyCrudAPI.Models;
using SimplyCrudAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace SimplyCrudAPI.Data
{
    public class UserAPIDbContext : DbContext
    {
        public UserAPIDbContext(DbContextOptions<UserAPIDbContext> options) : base(options) 
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<LogCheckLogin> LogCheckLogins { get; set; }
    }
}
