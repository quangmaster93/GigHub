using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        public ApplicationDbContext _context { get; set; }
        public GigRepository Gigs { get; set; }
        public ArtistRepository Artists { get; set; }
        public AttendanceRepository Attendances { get; set; }
        public FollowingRepository Followings { get; set; }

        public UnitOfWork(ApplicationDbContext _context)
        {
            this._context = _context;
            Gigs=new GigRepository(_context);
            Artists=new ArtistRepository(_context);
            Attendances=new AttendanceRepository(_context);
            Followings=new FollowingRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}