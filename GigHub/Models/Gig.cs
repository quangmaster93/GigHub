using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace GigHub.Models
{
    [DataContract(IsReference = true)]
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime{ get; set; }

        [Required]
        [StringLength(250)]
        public string Venue { get; set; }
        
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        
        public ICollection<Notification> Notifications { get; set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
            Notifications=new Collection<Notification>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification()
            {
                DateTime = DateTime.Now,
                Gig = this,
                Type = NotificationType.GigCanceled
            };

            Notifications.Add(notification);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                notification.Notify(attendee);
            }
        }

        public void Update(DateTime dateTime,byte genreId,string venue)
        {
            var notification = new Notification()
            {
                DateTime = DateTime.Now,
                Gig = this,
                OriginalDateTime = this.DateTime,
                OriginalVenue = this.Venue,
                Type = NotificationType.GigUpdateed
            };
            Notifications.Add(notification);
            foreach (var atttendee in Attendances.Select(a=>a.Attendee))
            {
                notification.Notify(atttendee);
            }

            this.DateTime = dateTime;
            GenreId = genreId;
            Venue = venue;
        }
    }

}
























