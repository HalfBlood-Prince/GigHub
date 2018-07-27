using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigViewModel
    {
        public bool IsAuthenticated { get; set; }

        public IEnumerable<Gig> Gigs { get; set; }

        public string Heading { get; set; }

        public string SearchTerm { get; set; }

        public ILookup<int, Attendance> Attendaces { get; set; }

        public ILookup<string, Following> Followings { get; set; }
    }
}