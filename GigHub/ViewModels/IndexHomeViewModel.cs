using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class IndexHomeViewModel
    {
        public IEnumerable<Gig> Gigs { get; set; }
        public IEnumerable<int> AttendingGigs { get; set; }
    }
}