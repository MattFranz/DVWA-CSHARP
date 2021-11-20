using OWASP10_2021.Models;
using System.Threading.Tasks;

namespace OWASP10_2021.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
