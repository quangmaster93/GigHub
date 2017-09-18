using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        public ApplicationDbContext _context { get; set; }

        public NotificationsController()
        {
            _context=new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => new NotificationDto()
                {
                    DateTime = un.Notification.DateTime,
                    OriginalDateTime = un.Notification.OriginalDateTime,
                    OriginalVenue = un.Notification.OriginalVenue,
                    Type = un.Notification.Type,
                    Gig = new GigDto()
                    {
                        Id = un.Notification.Gig.Id,
                        DateTime = un.Notification.Gig.DateTime,
                        Venue = un.Notification.Gig.Venue,
                        Artist = new UserDto()
                        {
                            Id = un.Notification.Gig.ArtistId,
                            Name = un.Notification.Gig.Artist.Name,
                        }
                    }
                });

            //var notifications1 = _context.UserNotifications
            //    .Where(un => un.UserId == userId)
            //    .Select(un => un.Notification)
            //    .Include(u=>u.Gig).ToList();
            // _context.Entry(notifications1).Reference<Gig>().Load();

            return notifications;
        }

        public int GetCount()
        {
            var userId = User.Identity.GetUserId();
            var count = _context.UserNotifications.Count(un => un.UserId == userId && !un.IsRead);
            return count;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead);
            notifications.ForEach(n=>n.IsRead=true);
            _context.SaveChanges();
            return Ok();
        }
    }
}
