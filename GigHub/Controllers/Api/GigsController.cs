using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        public ApplicationDbContext _context;

        public GigsController()
        {
            _context=new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(GigDto dto)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g=>g.Attendances.Select(a=>a.Attendee))
                .FirstOrDefault(g => g.Id == dto.Id &&g.ArtistId==userId);

            if (gig != null && gig.IsCanceled)
            {
                return NotFound();
            }

            gig?.Cancel();

            _context.SaveChanges();
            return Ok();
        }
    }
}














