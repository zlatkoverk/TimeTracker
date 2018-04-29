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
        public DbSet<Project> Projects { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired(false);
            modelBuilder.Entity<User>().Property(user => user.Surname).IsRequired(false);
            modelBuilder.Entity<User>().Property(user => user.Username).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().HasMany(user => user.Projects);

            modelBuilder.Entity<Project>().HasKey(project => project.Id);
            modelBuilder.Entity<Project>().Property(project => project.Name).IsRequired();
            modelBuilder.Entity<Project>().Property(project => project.Description).IsRequired(false);
            modelBuilder.Entity<Project>().HasMany<Activity>(project => project.Activities);

            modelBuilder.Entity<Activity>().HasKey(activity => activity.Id);
            modelBuilder.Entity<Activity>().Property(activity => activity.Description).IsRequired(false);
            modelBuilder.Entity<Activity>().Property(activity => activity.StartTime).IsRequired();
            modelBuilder.Entity<Activity>().Property(activity => activity.EndTime).IsRequired();
        }
    }
}
