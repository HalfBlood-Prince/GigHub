using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendaceRepository _attendaceRepository;

        public HomeController()
        {
            _context = ApplicationDbContext.Create();
            _attendaceRepository = new AttendaceRepository(_context);

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index(string query = null)
        {
            var gigs = _context.Gigs.Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);


            if (!string.IsNullOrWhiteSpace(query))
            {
                gigs = gigs.Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Vanue.Contains(query));
            }


            var userId = User.Identity.GetUserId();
            var attendaces = _attendaceRepository.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var following = _context
                .Followings
                .Where(f => f.UserId == userId)
                .ToList()
                .ToLookup(f => f.ArtistId);

            var viewModel = new GigViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Gigs = gigs,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendaces = attendaces,
                Followings = following
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}