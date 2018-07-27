using System.Collections.Generic;

namespace GigHub.Core.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public List<Gig> Gigs { get; set; }
    }
}