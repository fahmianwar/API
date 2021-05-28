using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
            .HasOne(p => p.Account)
            .WithOne(a => a.Person)
            .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
            .HasOne(a => a.Profiling)
            .WithOne(p => p.Account)
            .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<Profiling>()
            .HasOne(p => p.Education)
            .WithMany(e => e.Profiling);

            modelBuilder.Entity<Education>()
           .HasOne(a => a.University)
           .WithMany(p => p.Education);
        }
    }
}
