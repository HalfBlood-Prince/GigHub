using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers.api
{

    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public GigsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(gp => gp.Attendee))
                .SingleOrDefault(g => g.Id == id && g.ArtistId == userId && !g.IsCancelled);

            if (gig == null)
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
