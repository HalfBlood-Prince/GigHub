using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfiguration
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(g => g.Id);

            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}