using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query=null)
        {
            var gigs = _context.Gigs.Include(g => g.Artist).Include(g=>g.Genre).Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            if (!query.IsNullOrEmpty())
            {
                gigs = gigs.Where(g => g.Artist.Name.Contains(query) || g.Venue.Contains(query) || g.Genre.Name.Contains(query));
            }

            IEnumerable<int> attendingGigs = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _context.Users
                    .Include(u => u.Gigs)
                    .FirstOrDefault(u => u.Id == userId);

                attendingGigs = user.Gigs.Select(g=>g.GigId).ToList();
            }
            
            var viewmodel=new IndexHomeViewModel()
            {
                Gigs = gigs,
                AttendingGigs = attendingGigs
            };
            return View(viewmodel);
        }

    }
}