using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Shared
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> Credentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired(false);
            modelBuilder.Entity<User>().Property(user => user.Surname).IsRequired(false);
            modelBuilder.Entity<User>().HasOne(user => user.Credentials);

            modelBuilder.Entity<UserCredentials>().HasKey(credentials => credentials.Username);
            modelBuilder.Entity<UserCredentials>().Property(credentials => credentials.PasswordHash).IsRequired();
        }
    }
}
