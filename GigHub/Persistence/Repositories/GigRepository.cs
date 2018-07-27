using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendances(int gigId)
        {
            return _context.Gigs
                 .Include(g => g.Attendances.Select(gp => gp.Attendee))
                 .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetActiveFutureGigsWithGenre(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }


        public Gig GetGigById(int gigId)
        {
            return _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Artist)
                    .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}