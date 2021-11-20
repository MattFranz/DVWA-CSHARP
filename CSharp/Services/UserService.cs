using OWASP10_2021.Models;
using System.Threading.Tasks;

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

            switch (username.ToLower())
            {
                case "admin":
                    {
                        if (password == "admin")
                            return await Task.FromResult(new User() { Id="admin", Username = "Super Admin" });
                        break;
                    }
            }
            return await Task.FromResult<User>(null);
        }
    }
}
