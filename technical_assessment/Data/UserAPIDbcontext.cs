using Microsoft.EntityFrameworkCore;
using Technical_assessment.models;

namespace Technical_assessment.Data
{
    public class UserAPIDbcontext : DbContext
    {
        //creating context and adding tables
        public UserAPIDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> user { get; set; }
        public DbSet<Task_> task { get; set; }
    }
}
