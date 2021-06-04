using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class IdentityDataContext : IdentityDbContext
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
          : base(options)
        {
            // Tells EF to make sure Database exists prior to making calls
            // If it doesn't exist, EF will generate and execute the SQL schema
            // required to create it.
            Database.EnsureCreated();
        }
    }
}
