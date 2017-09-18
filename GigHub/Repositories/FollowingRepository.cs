using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        public ApplicationDbContext _context { get; set; }

        public FollowingRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public Following GetFollowing(string followeeId, string followerId)
        {
            return _context.Followings.FirstOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }
    }
}