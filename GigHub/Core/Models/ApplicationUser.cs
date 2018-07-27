using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public List<Gig> Gigs { get; set; }


        public List<Following> Followers { get; set; }


        public List<Following> Artists { get; set; }


        public ICollection<UserNotification> UserNotifications { get; set; }


        public ApplicationUser()
        {
            Gigs = new List<Gig>();
            Followers = new List<Following>();
            Artists = new List<Following>();
            UserNotifications = new List<UserNotification>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            var userNotification = new UserNotification(Id, notification);

            UserNotifications.Add(userNotification);
        }
    }
}