using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }


        public NotificationType Type { get; set; }


        public DateTime? OrginalDateTime { get; set; }

        public string OrginalVanue { get; set; }


        public GigDto Gig { get; set; }

    }
}