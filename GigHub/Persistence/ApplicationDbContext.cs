using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistance.EntityConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public DbSet<Gig> Gigs { get; set; }


        public DbSet<Genre> Genres { get; set; }
  

        public DbSet<Attendance> Attendances { get; set; }


        public DbSet<Following> Followings { get; set; }


        public DbSet<Notification> Notifications { get; set; }


        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new AttendaceConfiguration());
            modelBuilder.Configurations.Add(new FollowingConfiguration());
            modelBuilder.Configurations.Add(new UserNotificationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}