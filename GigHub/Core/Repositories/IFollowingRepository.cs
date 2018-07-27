using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFollowingsByUserId(string userId);
        Following GetFollowing(string userId, string artistId);
    }
}