using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        public ApplicationDbContext _context { get; set; }
        public UnitOfWork _unitOfWork { get; set; }

        public GigsController()
        {
            _context=new ApplicationDbContext();
            _unitOfWork=new UnitOfWork(_context);
        }
        // GET: Gigs
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel {Genres = _context.Genres,Heading = "Add a Gig"};
            return View("GigForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres;
                return View("GigForm", viewModel);
            }
            var artistId = User.Identity.GetUserId();
            var gig = new Gig()
            {
                ArtistId = artistId,
                DateTime = viewModel.GetDateTime(),
                GenreId=viewModel.Genre,
                Venue=viewModel.Venue
            };
            //_context.Gigs.Add(gig);
            //_context.SaveChanges();
            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine");
        }

        public ActionResult Edit(int gigId)
        {
            var gig = _unitOfWork.Gigs.GetGigById(gigId);
            if (gig==null)
            {
                return HttpNotFound();
            }
            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new GigFormViewModel
            {
                Id=gig.Id,
                Genres = _context.Genres,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("dd MMM yyyy"),
                Time=gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Heading = "Edit the Gig"
            };
            return View("GigForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres;
                return View("GigForm", viewModel);
            }
            var gig = _unitOfWork.Gigs.GetGigWithAttendee(viewModel.Id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            gig.Update(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

            _unitOfWork.Complete();
            return RedirectToAction("Mine");
        }

        public ActionResult Attending()
        {
            var gigs = _unitOfWork.Gigs.GetGigsUserAttending(User.Identity.GetUserId());
            return View(gigs);
        }

        public ActionResult Following()
        {
            var followees = _unitOfWork.Artists.GetArtistUserFollowing(User.Identity.GetUserId());
            return View(followees);
        }

        public ActionResult Mine()
        {

            var gigs = _unitOfWork.Gigs.GetFutureGig(User.Identity.GetUserId()).Include(g => g.Genre);
           
            return View(gigs);
        }

        [AllowAnonymous]
        public ActionResult Detail(int gigId)
        {
            var gig = _unitOfWork.Gigs.GetGigById(gigId);
            if (gig==null)
            {
                return HttpNotFound();
            }

            var isAttending = false;
            var isFollowing = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                isAttending = _unitOfWork.Attendances.GetAttendance(userId, gig.Id) != null;
                isFollowing = _unitOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;
            }

            var viewModel = new DetailViewModel(gig,isAttending,isFollowing);
            return View(viewModel);
        }
    }
}






























