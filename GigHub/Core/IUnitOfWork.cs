using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository GigRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        IGenreRepository GenreRepository { get; }
        IAttendaceRepository AttendaceRepository { get; }
        void Complete();
    }
}