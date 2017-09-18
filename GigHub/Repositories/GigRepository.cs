using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        public ApplicationDbContext _context { get; set; }

        public GigRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Gig GetGigById(int gigId)
        {
            return _context.Gigs.Include(g=>g.Artist).FirstOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendee(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre);
        }

        public IQueryable<Gig> GetFutureGig(string userId)
        {
            return _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}