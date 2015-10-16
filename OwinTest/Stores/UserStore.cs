using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using OwinTest.Models;

namespace OwinTest.Stores
{
    public class UserStore : IUserPasswordStore<ApplicationUser, Guid>, IUserLockoutStore<ApplicationUser, Guid>, IUserTwoFactorStore<ApplicationUser, Guid>
    {
        private static readonly List<ApplicationUser> _users = new List<ApplicationUser>(); 

        public void Dispose()
        {
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_users.All(x => x.Id != user.Id))
                {
                    _users.Add(user);
                }
            });
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                _users.Remove(_users.Single(x => x.Id == user.Id));
                _users.Add(user);
            });
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                _users.Remove(_users.Single(x => x.Id == user.Id));
            });
        }

        public Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            return Task.Factory.StartNew(() => _users.SingleOrDefault(x => x.Id == userId));
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => _users.SingleOrDefault(x => x.UserName == userName));
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PasswordHash = passwordHash;
            });
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => String.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => user.LockoutEnd);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            return Task.Factory.StartNew(() =>
            {
                user.LockoutEnd = lockoutEnd;
            });
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => user.IncrementFailedAttepts());
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(user.ResetFailedAttempts);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => user.FailedAttempts);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => true);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.Factory.StartNew(() => {});
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(false);
        }
    }
}