using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfiguration
{
    public class AttendaceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendaceConfiguration()
        {
            HasKey(a => new {a.AttendeeId, a.GigId});


            HasRequired(a => a.Gig)
                .WithMany(g => g.Attendances)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.Attendee)
                .WithMany();
        }
    }
}