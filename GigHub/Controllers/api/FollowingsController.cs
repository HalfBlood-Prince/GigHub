using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        public IHttpActionResult AddFollower(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.ArtistId == dto.ArtistId && f.UserId == userId))
                return BadRequest("Already Following");

            var following = new Following
            {
                ArtistId = dto.ArtistId,
                UserId = userId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();


            return Ok();
        }
    }
}
