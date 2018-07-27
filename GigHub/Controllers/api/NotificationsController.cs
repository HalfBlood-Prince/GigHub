using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class NotificationsController : ApiController
    {

        private readonly ApplicationDbContext _context;


        public NotificationsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        public IEnumerable<NotificationDto> GetNotification()
        {
            var userID = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userID && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }


        [HttpPut]
        public IHttpActionResult MarkRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();


            notifications.ForEach(up => up.Read());

            _context.SaveChanges();

            return Ok();
        }

    }
}
