using System;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; private set; }


        public NotificationType Type { get; private set; }


        public DateTime? OrginalDateTime { get; private set; }

        public string OrginalVanue { get; private set; }


        public Gig Gig { get; private set; }


        public int GigId { get; private set; }

        protected Notification()
        {
        }

        private Notification(int gigId, NotificationType type)
        {
            if(gigId == 0)
                throw new ArgumentNullException("gigId");


            GigId = gigId;
            Type = type;
            DateTime = DateTime.Now;

        }


        public static Notification GigCreate(int id)
        {
            return new Notification(id,NotificationType.GigCreated);
        }

        public static Notification GigUpdate(int id, DateTime oringinalDate, string originalVanue)
        {
            var noti = new Notification(id, NotificationType.GigUpdated)
            {
                OrginalDateTime = oringinalDate,
                OrginalVanue = originalVanue
            };

            return noti;
        }


        public static Notification GigCancelled(int id)
        {
            return new Notification(id,NotificationType.GigCancelled);
        }
    }
}