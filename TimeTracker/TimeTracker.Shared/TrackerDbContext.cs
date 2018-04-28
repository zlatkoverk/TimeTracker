using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Shared
{
    public class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> Credentials { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeInterval> TimeIntervals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired(false);
            modelBuilder.Entity<User>().Property(user => user.Surname).IsRequired(false);
            modelBuilder.Entity<User>().HasOne(user => user.Credentials);
            modelBuilder.Entity<User>().HasMany(user => user.Projects);

            modelBuilder.Entity<UserCredentials>().HasKey(credentials => credentials.Username);
            modelBuilder.Entity<UserCredentials>().Property(credentials => credentials.PasswordHash).IsRequired();

            modelBuilder.Entity<Project>().HasKey(project => project.Id);
            modelBuilder.Entity<Project>().Property(project => project.Name).IsRequired();
            modelBuilder.Entity<Project>().Property(project => project.Description).IsRequired(false);
            modelBuilder.Entity<Project>().HasMany<TimeInterval>(project => project.WorkIntervals).WithOne(interval => interval.Project);

            modelBuilder.Entity<TimeInterval>().HasKey(interval => interval.Id);
        }
    }
}
