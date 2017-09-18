﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web.Http;

namespace GigHub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>().HasRequired(a => a.Gig).WithMany(g=>g.Attendances).WillCascadeOnDelete(false);
            modelBuilder.Entity<Following>().HasRequired(f => f.Followee).WithMany(f=>f.Followers).WillCascadeOnDelete(false);
            modelBuilder.Entity<Following>().HasRequired(f => f.Follower).WithMany(f => f.Followees).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}












