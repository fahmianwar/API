using API.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
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
        */
            modelBuilder.Entity<Person>()
            .HasOne(p => p.Account)
            .WithOne(a => a.Person)
            .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
            .HasOne(a => a.Profiling)
            .WithOne(p => p.Account)
            .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.NIK, ar.RoleId });

            modelBuilder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(ar => ar.AccountRole)
            .HasForeignKey(a => a.NIK);

            modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AccountRole)
            .HasForeignKey(ar => ar.RoleId);

            /*
            modelBuilder.Entity<Role>()
            .HasMany(p => p.AccountRole)
            .WithMany(e => e.Role);
            */
            modelBuilder.Entity<Profiling>()
            .HasOne(p => p.Education)
            .WithMany(e => e.Profiling);
            /*
            modelBuilder.Entity<Education>()
                .HasMany(e => e.Profiling)
                .WithOne(p => p.Education);
            */
            /*
            modelBuilder.Entity<Education>()
           .HasOne(a => a.University)
           .WithMany(p => p.Education);
             */

            modelBuilder.Entity<University>()
           .HasMany(u => u.Education)
           .WithOne(e => e.University);

        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        */
    }
}
