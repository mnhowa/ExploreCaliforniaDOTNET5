using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogDataContext(DbContextOptions<BlogDataContext> options)
            : base(options)
        {
            // Tells EF to make sure Database exists prior to making calls
            // If it doesn't exist, EF will generate and execute the SQL schema
            // required to create it.
            Database.EnsureCreated();
        }
    }
}
