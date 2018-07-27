namespace GigHub.Core.Models
{
    public class Following
    {
        public ApplicationUser Artist { get; set; }

        public ApplicationUser User { get; set; }

        public string ArtistId { get; set; }

        public string UserId { get; set; }
    }
}