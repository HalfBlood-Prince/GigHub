using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Following> GetFollowingsByUserId(string userId)
        {
            return _context
                .Followings
                .Where(f => f.UserId == userId)
                .ToList();
        }


        public Following GetFollowing(string userId, string artistId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.ArtistId == artistId && f.UserId == userId);
        }
    }
}