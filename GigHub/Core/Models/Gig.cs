using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.ViewModels;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public DateTime DateTime { get; set; }

        public string Vanue { get; set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }


        public List<Notification> Notifications { get; set; }

        public ICollection<Attendance> Attendances { get; set; }


        public Gig()
        {
            Notifications = new List<Notification>();
            Attendances = new List<Attendance>();
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = Notification.GigCancelled(Id);

            foreach (var attendee in Attendances.Select(g => g.Attendee))
            {
                attendee.Notify(notification);
            }
        }


        public void Update(GigFormViewModel viewModel)
        {
            var notification = Notification.GigUpdate(Id, DateTime, Vanue);

            GenreId = viewModel.GenreId;
            Vanue = viewModel.Venue;
            DateTime = viewModel.GetDateTime();


            foreach (var attendee in Attendances.Select(g => g.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}