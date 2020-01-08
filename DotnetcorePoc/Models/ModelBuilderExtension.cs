using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetcorePoc.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    Name = "Palden",
                    Email = "palden@gmail.com",
                    Department = Dept.IT
                },
                new Member
                {
                    Id = 2,
                    Name = "Chime",
                    Email = "chime@gmail.com",
                    Department = Dept.IT
                }
            );
        }
    }
}
