namespace TechAndTools.Services
{
    using Castle.Core.Internal;
    using Contracts;
    using Data;
    using Data.Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly TechAndToolsDbContext context;

        public UserService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public TechAndToolsUser GetUserByUsername(string username)
        {
            var userFromDb =  this.context.Users
                .FirstOrDefault(x => x.UserName == username);

            return userFromDb;
        }

        public TechAndToolsUser GetUserById(string userId)
        {
            var userFromDb = this.context.Users
                .Find(userId);

            return userFromDb;
        }

        public async Task<bool> EditFirstName(TechAndToolsUser user, string firstName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (firstName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            user.FirstName = firstName;
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditLastName(TechAndToolsUser user, string lastName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (lastName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.LastName = lastName;
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
