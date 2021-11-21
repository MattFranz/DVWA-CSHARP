using OWASP10_2021.Models;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OWASP10_2021.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return await Task.FromResult<User>(null);
            if (string.IsNullOrEmpty(password))
                return await Task.FromResult<User>(null);

            return await GetUser(username, password);
        }

        private async Task<User> GetUser(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentException();

            user = user.ToLower().Trim();
            var userText = await File.ReadAllTextAsync(Path.Combine(Startup.WebRootPath, "_private/passwords_admin.txt"));
            var userInfo = userText
                .Split("\n", System.StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new User()
                {
                    Id = SplitAndGetOrdinal(x, ",", 0),
                    Username = SplitAndGetOrdinal(x, ",", 0),
                    PasswordHash = SplitAndGetOrdinal(x, ",", 1),
                    Role = SplitAndGetOrdinal(x, ",", 2)
                })
                .Where(x => x.Username == user && Encryption.ComputeHash(password) == x.PasswordHash)
                .SingleOrDefault();

            return userInfo;
        }

        private string SplitAndGetOrdinal(string theString, string seperator, int ordinal)
        {
            return theString.Split(seperator, System.StringSplitOptions.RemoveEmptyEntries)[ordinal];
        }
    }
}