using System;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public bool IsLoggedIn { get; set; }

        public bool IsFollowing { get; set; }

        public DateTime DateTime { get; set; }

        public string Vanue { get; set; }

        public bool IsGoing { get; set; }

        public string ArtistName { get; set; }
    }
}