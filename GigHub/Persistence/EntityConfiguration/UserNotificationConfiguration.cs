using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfiguration
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(n => new {n.UserId, n.NotificationId});

            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(n => n.UserId)
                .WillCascadeOnDelete(false);


            HasRequired(n => n.Notification)
                .WithMany()
                .HasForeignKey(n => n.NotificationId)
                .WillCascadeOnDelete(false);
        }
    }
}