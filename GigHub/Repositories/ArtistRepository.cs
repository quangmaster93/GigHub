using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class ArtistRepository
    {
        public ApplicationDbContext _context { get; set; }

        public ArtistRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<ApplicationUser> GetArtistUserFollowing(string userId)
        {
            return _context.Followings.Where(f => f.FollowerId == userId).Select(f => f.Followee);
        }
    }
}