using SD.Application.Extensions;
using System.Net;
using System.Security.Cryptography.Xml;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Services;

namespace SD.Application.Services
{
    public class UserService : IUserService
    {
        /* Mockups */
        List<User> _users = new()
        {
            new User { Id = new Guid("dec29c83-e98a-4b94-b6d0-f53e10effd80"), FirstName = "Max", LastName="Musterman", UserName="MovieUser", Password = new NetworkCredential("ADMIN", "733295Sa").SecurePassword},
            new User { Id = new Guid("97d5b17b-e10e-45e3-a10e-03afae858213"), FirstName = "Jo", LastName="Rip", UserName="Guest", Password = new NetworkCredential("Guest", "secret").SecurePassword},
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = _users.SingleOrDefault(w => string.Compare(w.UserName,username, true) == 0
                                                && new NetworkCredential(w.UserName, w.Password).Password == password);
            if (user == null)
            {
                return user;
            }

            return await Task.FromResult(user.WithoutPassword());
        }
    }
}
