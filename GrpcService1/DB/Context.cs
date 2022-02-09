using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcService1.DB
{
    class Context : DbContext
    {
        public Context()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=sqllitedb1.db");

    
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(p => p.Surname).IsRequired();
            modelBuilder.Entity<Employee>().Property(p => p.Birthday).IsRequired();
            modelBuilder.Entity<Employee>().Property(p => p.Sex).IsRequired();


            modelBuilder.Entity<Employee>().HasIndex(b => b.Surname);


        }
       public DbSet<Employee> Employees { get; set; }

    }
}
