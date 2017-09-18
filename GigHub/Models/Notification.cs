using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Management;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }

        public Notification()
        {
            UserNotifications=new Collection<UserNotification>();
        }

        internal void Notify(ApplicationUser attendee)
        {
            var userNitification = new UserNotification()
            {
                Notification = this,
                User = attendee
            };
            UserNotifications.Add(userNitification);
        }
    }
}

















