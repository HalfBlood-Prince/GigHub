using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithAttendances(int gigId);
        IEnumerable<Gig> GetActiveFutureGigsWithGenre(string userId);
        Gig GetGigById(int gigId);
        void Add(Gig gig);
    }
}