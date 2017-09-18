using GigHub.Models;
using NUnit.Framework.Constraints;

namespace GigHub.ViewModels
{
    public class DetailViewModel
    {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
        public DetailViewModel(Gig gig, bool isAttending, bool isFollowing)
        {
            Gig = gig;
            IsAttending = isAttending;
            IsFollowing = isFollowing;
        }
    }
}