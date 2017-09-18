using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        public ApplicationDbContext Context { get; set; }

        public FollowingsController()
        {
               Context=new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var isFollowed = Context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
            if (isFollowed)
            {
                return BadRequest("This Artist already followed");
            };
            var following=new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };
            Context.Followings.Add(following);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowing(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var following =Context.Followings.FirstOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
            if (following==null)
            {
                return NotFound();
            }
            Context.Followings.Remove(following);
            Context.SaveChanges();
            return Ok();
        }
    }
}








