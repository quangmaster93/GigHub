using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        public ApplicationDbContext _context { get; set; }

        public AttendanceRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public Attendance GetAttendance(string attendeeId, int gigId)
        {
            return _context.Attendances.FirstOrDefault(a=>a.AttendeeId==attendeeId && a.GigId==gigId);
        }
    }
}