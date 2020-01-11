using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetcorePoc.Models
{
    // DbContext class manages the db connections.
    // It retrieves and save data in db.
    public class AppDbContext: DbContext
    {
        // DbContextOptions class carries config infos' like 
        // connection strings to use, 
        // db provider to use and etc
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            :base(options)
        {

        }

        // Set up DbSet property for each entity type : Member
        // the property, Members is use to query and save instances of Member class   
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
