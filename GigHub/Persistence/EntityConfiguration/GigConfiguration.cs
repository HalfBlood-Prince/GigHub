using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfiguration
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            HasKey(g => g.Id);



            Property(g => g.ArtistId)
                .IsRequired();

            Property(g => g.GenreId)
                .IsRequired();

            Property(g => g.Vanue)
                .IsRequired()
                .HasMaxLength(255);


            HasRequired(g => g.Genre)
                .WithMany(g => g.Gigs)
                .HasForeignKey(g => g.GenreId);

            HasRequired(g => g.Artist)
                .WithMany(a => a.Gigs)
                .HasForeignKey(g => g.ArtistId);

            HasMany(g => g.Notifications)
                .WithRequired(n => n.Gig)
                .HasForeignKey(n => n.GigId)
                .WillCascadeOnDelete(false);
        }
    }
}