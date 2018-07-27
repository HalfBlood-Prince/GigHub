using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfiguration
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new {f.ArtistId, f.UserId});


            HasRequired(f => f.Artist)
                .WithMany(a => a.Followers)
                .HasForeignKey(f => f.ArtistId)
                .WillCascadeOnDelete(false);


            HasRequired(f => f.User)
                .WithMany(u => u.Artists)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}