using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository GigRepository { get; private set; }
        public IFollowingRepository FollowingRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public IAttendaceRepository AttendaceRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            GigRepository = new GigRepository(_context);
            FollowingRepository = new FollowingRepository(_context);
            GenreRepository = new GenreRepository(_context);
            AttendaceRepository = new AttendaceRepository(_context);
        }



        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}