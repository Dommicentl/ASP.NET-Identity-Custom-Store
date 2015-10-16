using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OwinTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IUser<Guid>
    {
        public ApplicationUser()
        {
            Id = new Guid();
            FailedAttempts = 0;
        }

        public string PasswordHash { get; set; }

        public Guid Id { get; private set; }
        public string UserName { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public int FailedAttempts { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public int IncrementFailedAttepts()
        {
            return FailedAttempts++;
        }

        public void ResetFailedAttempts()
        {
            FailedAttempts = 0;
        }
    }
}