using GigHub.Persistance;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.GigRepository.GetActiveFutureGigsWithGenre(userId);

            return View(gigs);
        }



        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = _unitOfWork.GenreRepository.GetAllGenres()
            };
            return View("GigForm", viewModel);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.GetAllGenres();

                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                GenreId = viewModel.GenreId,
                ArtistId = User.Identity.GetUserId(),
                Vanue = viewModel.Venue,
                DateTime = viewModel.GetDateTime()
            };

            _unitOfWork.GigRepository.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.GigRepository.GetGigById(id);


            if (gig == null || gig.ArtistId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit",
                Genres = _unitOfWork.GenreRepository.GetAllGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                GenreId = gig.GenreId,
                Venue = gig.Vanue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.GetAllGenres();

                return View("GigForm", viewModel);
            }


            var gig = _unitOfWork.GigRepository.GetGigWithAttendances(viewModel.Id);

            if (gig == null)
                return HttpNotFound();


            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Update(viewModel);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();


            var following = _unitOfWork.FollowingRepository
                    .GetFollowingsByUserId(userId)
                    .ToLookup(f => f.ArtistId);

            var viewModel = new GigViewModel
            {
                Gigs = _unitOfWork.GigRepository.GetGigUserAttending(userId),
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Attending",
                Attendaces = _unitOfWork.AttendaceRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId),
                Followings = following
            };

            return View("Gigs", viewModel);
        }




        public ActionResult Search(GigViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.GigRepository.GetGigById(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel
            {
                Vanue = gig.Vanue,

                DateTime = gig.DateTime,

                ArtistName = gig.Artist.Name

            };


            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsLoggedIn = User.Identity.IsAuthenticated;

                viewModel.IsFollowing = _unitOfWork.FollowingRepository.GetFollowing(userId, gig.ArtistId) != null;

                viewModel.IsGoing = _unitOfWork.AttendaceRepository.GetAttendance(userId, gig.Id) != null;

            }


            return View(viewModel);
        }
    }
}