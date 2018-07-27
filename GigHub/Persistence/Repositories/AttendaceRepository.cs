using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class AttendaceRepository : IAttendaceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context
                .Attendances
                .Include(a => a.Gig)
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }


        public Attendance GetAttendance(string userId, int gigId)
        {
            return _context
                .Attendances
                .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }
    }
}