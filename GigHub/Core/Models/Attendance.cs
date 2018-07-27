namespace GigHub.Core.Models
{
    public class Attendance
    {
        public ApplicationUser Attendee { get; set; }

        public Gig Gig { get; set; }


        public int GigId { get; set; }

        public string AttendeeId { get; set; }

    }
}