using Wifi.SD.Core.Entities;

namespace SD.Application.Extensions
{
    public static class UserExtension
    {
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users) 
        {
            return users.Select(s => s.WithoutPassword());
        }

    }
}
