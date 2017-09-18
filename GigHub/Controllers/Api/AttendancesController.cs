using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        public ApplicationDbContext Context { get; set; }

        public AttendancesController()
        {
                Context=new ApplicationDbContext();
        }     


        [HttpGet]
        public IHttpActionResult GetValue()
        {
            return Ok("123");
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            
                var isExists = Context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
                if (isExists)
                {
                    return BadRequest("The attendance already exists");
                }
            
            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            Context.Attendances.Add(attendance);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = Context.Attendances.FirstOrDefault(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (attendance==null)
            {
                return NotFound();
            }

            Context.Attendances.Remove(attendance);
            Context.SaveChanges();
            return Ok();
        }
    }
}




















