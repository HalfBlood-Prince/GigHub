using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers
{
    public class FollowController : Controller
    {

        private readonly ApplicationDbContext _context;

        public FollowController()
        {
            _context = ApplicationDbContext.Create();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Follow
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();


            var artists = _context.Followings.Where(f => f.UserId == userId)
                .Select(f => f.Artist);



            return View(artists);
        }
    }
}