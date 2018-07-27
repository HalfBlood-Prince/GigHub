using System;

namespace GigHub.Core.Models
{
    public class UserNotification
    {

        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }


        protected UserNotification()
        {
        }



        public UserNotification(string id, Notification notification)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (notification == null)
                throw new ArgumentNullException("notification");

            UserId = id;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}